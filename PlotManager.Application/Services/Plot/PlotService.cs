using Mapster;
using PlotManager.Application.Repositories.RepositoryManager;
using PlotManager.Contracts;
using PlotManager.Contracts.Feature;
using PlotManager.Contracts.Plot;
using PlotManager.Domain.Entities;
using PlotManager.Domain.Exceptions;
using PlotManager.Security.FileScanner;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlotManager.Application.Services
{
    public class PlotService : RepositoryServiceBase, IPlotService
    {
        public PlotService(IRepositoryManager repositoryManager) : base(repositoryManager)
        {
        }

        public async Task<PlotDTO> CreateAsync(Guid plotComplexId, PlotForCreationDTO plotForCreationDTO, CancellationToken cancellationToken = default)
        {
            var plotComplex = await RepositoryManager.PlotComplexRepository.GetPlotComplexByIdAsync(plotComplexId);
            if (plotComplex == null)
            {
                throw new NotFoundException("");//TODO ADD MESSAGE
            }
            var plot = plotForCreationDTO.Adapt<Plot>();
            plot.PlotFeatures = new List<PlotFeatures>();
            foreach (var d in plotForCreationDTO.Features)
            {
                plot.PlotFeatures.Add(new PlotFeatures() { FeatureId = d.Id, PlotId = plot.Id });//WTF, wasn't able create a mapping
            }
            plotComplex.Plots.Add(plot);
            //this.RepositoryManager.PlotRepository.CreatePlot(plot);
            await this.RepositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
            return plot.Adapt<PlotDTO>();
        }

        public async Task<PlotDTO> CreateAsync(PlotForCreationDTO plotForCreationDTO, CancellationToken cancellationToken = default)
        {
            var plot = plotForCreationDTO.Adapt<Plot>();
            plot.PlotFeatures = new List<PlotFeatures>();
            foreach (var d in plotForCreationDTO.Features)
            {
                plot.PlotFeatures.Add(new PlotFeatures() { FeatureId = d.Id, PlotId = plot.Id });//WTF, wasn't able create a mapping
            }
            this.RepositoryManager.PlotRepository.CreatePlot(plot);
            await this.RepositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
            return plot.Adapt<PlotDTO>();
        }

        public async Task UpdateAsync(Guid id, PlotForUpdateDTO plotForUpdateDTO, CancellationToken cancellationToken = default)
        {
            var plot = await RepositoryManager.PlotRepository.GetPlotByIdAsync(id, cancellationToken);
            if (plot is null)
            {
                throw new NotFoundException($"Plot Complex with identifier {id} was not found");
            }
            plot.PlotFeatures = new List<PlotFeatures>();
            foreach (var featureDTO in plotForUpdateDTO.Features)
            {
                if (!plot.PlotFeatures.Any(x => x.FeatureId == featureDTO.Id))
                {
                    plot.PlotFeatures.Add(new PlotFeatures() { FeatureId = featureDTO.Id, PlotId = plot.Id });
                }
            }
            plot.OrdinalNumber = plotForUpdateDTO.OrdinalNumber;
            RepositoryManager.PlotRepository.UpdatePlot(plot);
            await RepositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var plot = await RepositoryManager.PlotRepository.GetPlotByIdAsync(id, cancellationToken);
            if (plot is null)
            {
                throw new NotFoundException($"Plot Complex with identifier {id} was not found");
            }
            RepositoryManager.PlotRepository.DeletePlot(plot);
            await RepositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<PagedList<PlotDTO>> GetAsync(Guid plotComplexId, PlotResourceParameters plotResourceParameters, CancellationToken cancellationToken = default)
        {
            var plots = await RepositoryManager.PlotRepository.GetPlotsAsync(plotComplexId, plotResourceParameters, cancellationToken);
            var plotDTOs = new PagedList<PlotDTO>(plots.Select(x => new PlotDTO() { Id = x.Id, OrdinalNumber = x.OrdinalNumber }).ToList(), plots.TotalCount, plots.CurrentPage, plots.PageSize);
            return plotDTOs;
        }

        public async Task<PlotDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var plot = await RepositoryManager.PlotRepository.GetPlotByIdAsync(id, cancellationToken);
            if (plot is null)
            {
                throw new NotFoundException($"Plot Complex with identifier {id} was not found");
            }
            var config = new TypeAdapterConfig();
            config.NewConfig<Plot, PlotDTO>().Map(dest => dest.Features, src => src.PlotFeatures);
            config.NewConfig<PlotFeatures, FeatureDTO>()
                    .Map(dest => dest.Id, src => src.Feature.Id)
                    .Map(dest => dest.Name, src => src.Feature.Name);
            return plot.Adapt<Plot, PlotDTO>(config);
        }

        public async Task ImportFromFile(string filePath, Guid plotComplexId, CancellationToken cancellationToken = default)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }
            try
            {
                var mimeType = FileMimeTypeScanner.GetMimeFromFile(filePath);
                if (mimeType != MimeTypesHeleper.CSV && mimeType != MimeTypesHeleper.Excel && mimeType != MimeTypesHeleper.TextPlain)
                {
                    throw new WrongFileTypeException("Wrong file type. Expected a CSV file.");
                }
                var plotImportFileParser = new PlotFileImporterCSV(RepositoryManager);
                var plots = await plotImportFileParser.ParseImportFile(filePath);
                foreach (var plot in plots)
                {
                    plot.PlotComplexId = plotComplexId;
                    this.RepositoryManager.PlotRepository.CreatePlot(plot);
                }
                await this.RepositoryManager.UnitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
            finally
            {
                File.Delete(filePath);
            }
        }

        public async Task<MemoryStream> GenerateImportFile()
        {
            var plotFileImporterCSV = new PlotFileImporterCSV(RepositoryManager);
            return await plotFileImporterCSV.GenerateImportFile();
        }
    }
}