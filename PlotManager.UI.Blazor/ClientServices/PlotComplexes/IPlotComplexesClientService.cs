using PlotManager.Contracts.PlotComplex;

namespace PlotManager.UI.Blazor.ClientServices.PlotComplexes
{
    public interface IPlotComplexesClientService
    {
        Task<PlotComplexDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<PlotComplexDTO>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<PlotComplexDTO> CreateAsync(PlotComplexForCreationDTO plotComplexForCreationDTO, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Guid id, PlotComplexForUpdateDTO plotComplexForUpdateDTO, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
