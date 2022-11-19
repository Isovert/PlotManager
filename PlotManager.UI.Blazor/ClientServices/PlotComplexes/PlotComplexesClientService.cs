using PlotManager.Contracts.PlotComplex;
using PlotManager.UI.Blazor.ClientServices.Security;
using PlotManager.UI.Blazor.HttpClients.PlotManagerAPI;

namespace PlotManager.UI.Blazor.ClientServices.PlotComplexes
{
    public class PlotComplexesClientService : IPlotComplexesClientService
    {
        private readonly IPlotManagerAPIClient _plotManagerAPIHttpClient;
        private readonly IAddBearerTokenService _addBearerTokenService;

        public PlotComplexesClientService(IPlotManagerAPIClient plotManagerAPIHttpClient,
                                          IAddBearerTokenService addBearerTokenService)
        {
            _plotManagerAPIHttpClient = plotManagerAPIHttpClient;
            _addBearerTokenService = addBearerTokenService;
        }

        public async Task<PlotComplexDTO> CreateAsync(PlotComplexForCreationDTO plotComplexForCreationDTO, CancellationToken cancellationToken = default)
        {
            await _addBearerTokenService.AddBearerToken(_plotManagerAPIHttpClient);
            var result = await _plotManagerAPIHttpClient.CreatePlotComplexAsync(plotComplexForCreationDTO, cancellationToken);
            return result;
        }

        public async Task<bool> UpdateAsync(Guid id, PlotComplexForUpdateDTO plotComplexForUpdateDTO, CancellationToken cancellationToken = default)
        {
            await _addBearerTokenService.AddBearerToken(_plotManagerAPIHttpClient);
            var result = await _plotManagerAPIHttpClient.UpdatePlotComplexAsync(id, plotComplexForUpdateDTO, cancellationToken);
            return result;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _addBearerTokenService.AddBearerToken(_plotManagerAPIHttpClient);
            return await _plotManagerAPIHttpClient.DeletePlotComplexAsync(id, cancellationToken);
        }

        public async Task<List<PlotComplexDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            await _addBearerTokenService.AddBearerToken(_plotManagerAPIHttpClient);
            var plotComplexes = await _plotManagerAPIHttpClient.GetPlotComplexesAsync(cancellationToken);
            return plotComplexes.ToList();
        }

        public async Task<PlotComplexDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _addBearerTokenService.AddBearerToken(_plotManagerAPIHttpClient);
            var plotComplex = await _plotManagerAPIHttpClient.GetPlotComplexByIdAsync(id, cancellationToken);
            return plotComplex;
        }
    }
}
