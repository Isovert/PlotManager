using PlotManager.Contracts.Plot;

namespace PlotManager.UI.Blazor.ClientServices.Plots
{
    public interface IPlotsClientService
    {
        Task<PlotDTO> CreatePlotForPlotComplexAsync(Guid plotComplexId, PlotForCreationDTO plotForCreationDTO, CancellationToken cancellationToken = default);
        Task<bool> UpdatePlotAsync(Guid plotComplexId, Guid plotId, PlotForUpdateDTO plotForUpdateDTO, CancellationToken cancellationToken = default);
        Task<bool> DeletePlotAsync(Guid plotComplexId, Guid plotId, CancellationToken cancellationToken = default);
        Task<PlotDTO> GetByIdAsync(Guid plotComplexId, Guid plotId, CancellationToken cancellationToken = default);
        Task<List<PlotDTO>> GetAllByPlotComplexIdAsync(Guid plotComplexId, CancellationToken cancellationToken = default);
    }
}