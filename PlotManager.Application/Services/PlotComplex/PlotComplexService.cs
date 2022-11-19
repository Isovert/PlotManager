using Mapster;
using PlotManager.Application.Repositories.RepositoryManager;
using PlotManager.Contracts;
using PlotManager.Contracts.PlotComplex;
using PlotManager.Domain.Entities;
using PlotManager.Domain.Exceptions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlotManager.Application.Services
{
    public class PlotComplexService : RepositoryServiceBase, IPlotComplexService
    {
        public PlotComplexService(IRepositoryManager repositoryManager) : base(repositoryManager)
        {
        }

        public async Task<PlotComplexDTO> CreateAsync(PlotComplexForCreationDTO plotComplexForCreationDTO, CancellationToken cancellationToken = default)
        {
            var plotComplex = plotComplexForCreationDTO.Adapt<PlotComplex>();
            this.RepositoryManager.PlotComplexRepository.CreatePlotComplex(plotComplex);
            await this.RepositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
            return plotComplex.Adapt<PlotComplexDTO>();
        }

        public async Task UpdateAsync(Guid id, PlotComplexForUpdateDTO plotComplexForUpdateDTO, CancellationToken cancellationToken = default)
        {
            var plotComplex = await RepositoryManager.PlotComplexRepository.GetPlotComplexByIdAsync(id, cancellationToken);
            if (plotComplex is null)
            {
                throw new NotFoundException($"Plot Complex with identifier {id} was not found");
            }
            plotComplex.Name = plotComplexForUpdateDTO.Name;
            await RepositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var plotComplex = await RepositoryManager.PlotComplexRepository.GetPlotComplexByIdAsync(id, cancellationToken);
            if (plotComplex is null)
            {
                throw new NotFoundException($"Plot Complex with identifier {id} was not found");
            }
            RepositoryManager.PlotComplexRepository.DeletePlotComplex(plotComplex);
            await RepositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
                
        public async Task<PlotComplexDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var plotComplex = await RepositoryManager.PlotComplexRepository.GetPlotComplexByIdAsync(id, cancellationToken);
            if (plotComplex is null)
            {
                throw new NotFoundException($"Plot Complex with identifier {id} was not found");
            }
            return plotComplex.Adapt<PlotComplexDTO>();
        }

        public async Task<PagedList<PlotComplexDTO>> GetAsync(PlotComplexResourceParameters plotComplexResourceParameters, CancellationToken cancellationToken = default)
        {
            var plotComplexes = await RepositoryManager.PlotComplexRepository.GetPlotComplexesAsync(plotComplexResourceParameters, cancellationToken);
            var plotComplexesDtos = new PagedList<PlotComplexDTO>(plotComplexes.Select(x => new PlotComplexDTO() { Id = x.Id, Name = x.Name }).ToList(), plotComplexes.TotalCount, plotComplexes.CurrentPage, plotComplexes.PageSize);
            return plotComplexesDtos;
        }
    }
}