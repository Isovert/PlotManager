using PlotManager.Contracts;
using PlotManager.Contracts.PlotComplex;

namespace PlotManager.Application.Services
{
    public interface IPlotComplexService
    {
        Task<PlotComplexDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<PagedList<PlotComplexDTO>> GetAsync(PlotComplexResourceParameters plotComplexResourceParameters, CancellationToken cancellationToken = default);
        Task<PlotComplexDTO> CreateAsync(PlotComplexForCreationDTO plotComplexForCreationDTO, CancellationToken cancellationToken = default);
        Task UpdateAsync(Guid id, PlotComplexForUpdateDTO plotComplexForUpdateDTO, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}