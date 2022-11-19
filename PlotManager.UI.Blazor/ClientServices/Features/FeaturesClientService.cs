using PlotManager.Contracts.Feature;
using PlotManager.UI.Blazor.ClientServices.Security;
using PlotManager.UI.Blazor.HttpClients.PlotManagerAPI;

namespace PlotManager.UI.Blazor.ClientServices.Features
{
    public class FeaturesClientService : IFeaturesClientService
    {
        private readonly IPlotManagerAPIClient _plotManagerAPIHttpClient;
        private readonly IAddBearerTokenService _addBearerTokenService;

        public FeaturesClientService(IPlotManagerAPIClient plotManagerAPIHttpClient,
                                     IAddBearerTokenService addBearerTokenService)
        {
            _plotManagerAPIHttpClient = plotManagerAPIHttpClient;
            _addBearerTokenService = addBearerTokenService;
        }

        public async Task<List<FeatureDTO>> GetFeaturesAsync(FeatureResourceParameters featureResourceParameters, CancellationToken cancellationToken = default)
        {
            await _addBearerTokenService.AddBearerToken(_plotManagerAPIHttpClient);
            var features = await _plotManagerAPIHttpClient.GetFeaturesAsync(featureResourceParameters, cancellationToken);
            return features.ToList();
        }

        public async Task<FeatureDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _addBearerTokenService.AddBearerToken(_plotManagerAPIHttpClient);
            var feature = await _plotManagerAPIHttpClient.GetFeatureByID(id, cancellationToken);
            return feature;
        }

        public async Task<FeatureDTO> CreateAsync(FeatureForCreationDTO featureForCreationDTO, CancellationToken cancellationToken = default)
        {
            await _addBearerTokenService.AddBearerToken(_plotManagerAPIHttpClient);
            var feature = await _plotManagerAPIHttpClient.CreateFeatureAsync(featureForCreationDTO, cancellationToken);
            return feature;
        }

        public async Task<bool> UpdateAsync(Guid id, FeatureForUpdateDTO featureForUpdateDTO, CancellationToken cancellationToken = default)
        {
            await _addBearerTokenService.AddBearerToken(_plotManagerAPIHttpClient);
            return await _plotManagerAPIHttpClient.UpdateFeatureAsync(id, featureForUpdateDTO, cancellationToken);            
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _addBearerTokenService.AddBearerToken(_plotManagerAPIHttpClient);
            return await _plotManagerAPIHttpClient.DeleteFeatureAsync(id, cancellationToken);            
        }
    }
}