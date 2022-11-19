using PlotManager.Contracts.Plot;
using PlotManager.UI.Blazor.ClientServices.Security;
using PlotManager.UI.Blazor.HttpClients.PlotManagerAPI;

namespace PlotManager.UI.Blazor.ClientServices.Plots
{
    public class PlotsClientService : IPlotsClientService
    {
        private readonly IPlotManagerAPIClient _plotManagerAPIHttpClient;
        private readonly IAddBearerTokenService _addBearerTokenService;

        public PlotsClientService(IPlotManagerAPIClient plotManagerAPIHttpClient,
                                  IAddBearerTokenService addBearerTokenService)
        {
            _plotManagerAPIHttpClient = plotManagerAPIHttpClient;
            _addBearerTokenService = addBearerTokenService;
        }

        public async Task<PlotDTO> CreatePlotForPlotComplexAsync(Guid plotComplexId, PlotForCreationDTO plotForCreationDTO, CancellationToken cancellationToken = default)
        {
            await _addBearerTokenService.AddBearerToken(_plotManagerAPIHttpClient);
            var result = await _plotManagerAPIHttpClient.CreatePlotForPlotComplexAsync(plotComplexId, plotForCreationDTO, cancellationToken);
            return result;
        }

        public async Task<bool> UpdatePlotAsync(Guid plotComplexId, Guid plotId, PlotForUpdateDTO plotForUpdateDTO, CancellationToken cancellationToken = default)
        {
            await _addBearerTokenService.AddBearerToken(_plotManagerAPIHttpClient);
            var result = await _plotManagerAPIHttpClient.UpdatePlotAsync(plotComplexId, plotId, plotForUpdateDTO, cancellationToken);
            return result;
        }

        public async Task<bool> DeletePlotAsync(Guid plotComplexId, Guid plotId, CancellationToken cancellationToken = default)
        {
            await _addBearerTokenService.AddBearerToken(_plotManagerAPIHttpClient);
            var result = await _plotManagerAPIHttpClient.DeletePlotAsync(plotComplexId, plotId, cancellationToken);
            return result;
        }

        public async Task<PlotDTO> GetByIdAsync(Guid plotComplexId, Guid plotId, CancellationToken cancellationToken = default)
        {
            await _addBearerTokenService.AddBearerToken(_plotManagerAPIHttpClient);
            var result = await _plotManagerAPIHttpClient.GetPlotByIdAsync(plotComplexId, plotId, cancellationToken);
            return result;
        }

        public async Task<List<PlotDTO>> GetAllByPlotComplexIdAsync(Guid plotComplexId, CancellationToken cancellationToken = default)
        {
            await _addBearerTokenService.AddBearerToken(_plotManagerAPIHttpClient);
            var plotDTOs = await _plotManagerAPIHttpClient.GetPlotsByPlotComplexIdAsync(plotComplexId, cancellationToken);
            return plotDTOs.ToList();
        }

    }
}
