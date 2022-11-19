using PlotManager.Contracts;
using PlotManager.Contracts.Plot;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PlotManager.Application.Services
{
    public interface IPlotService
    {
        Task<PlotDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<PagedList<PlotDTO>> GetAsync(Guid plotComplexId, PlotResourceParameters plotResourceParameters, CancellationToken cancellationToken = default);
        Task<PlotDTO> CreateAsync(Guid plotComplexId, PlotForCreationDTO plotForCreationDTO, CancellationToken cancellationToken = default);
        Task UpdateAsync(Guid id, PlotForUpdateDTO plotForUpdateDTO, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task ImportFromFile(string filePath, Guid plotComplexId, CancellationToken cancellationToken = default);
        Task<MemoryStream> GenerateImportFile();
    }
}