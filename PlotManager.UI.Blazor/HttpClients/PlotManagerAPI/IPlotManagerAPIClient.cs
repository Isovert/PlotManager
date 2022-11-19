using PlotManager.Contracts;
using PlotManager.Contracts.Feature;
using PlotManager.Contracts.Plot;
using PlotManager.Contracts.PlotComplex;
using PlotManager.Security.Identity.Models;

namespace PlotManager.UI.Blazor.HttpClients.PlotManagerAPI
{
    public interface IPlotManagerAPIClient : IAPIClientBase
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest body, CancellationToken cancellationToken = default);
        Task<RegistrationResponse> RegisterAsync(RegistrationRequest body, CancellationToken cancellationToken = default);


        #region Features
        Task<FeatureDTO> CreateFeatureAsync(FeatureForCreationDTO featureForCreationDTO, CancellationToken cancellationToken);
        Task<bool> UpdateFeatureAsync(Guid id, FeatureForUpdateDTO featureForUpdateDTO, CancellationToken cancellationToken);
        Task<bool> DeleteFeatureAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<FeatureDTO>> GetFeaturesAsync(FeatureResourceParameters featureResourceParameters, CancellationToken cancellationToken = default);
        Task<FeatureDTO> GetFeatureByID(Guid id, CancellationToken cancellationToken);
        #endregion


        #region PlotComplexes
        Task<PlotComplexDTO> CreatePlotComplexAsync(PlotComplexForCreationDTO plotComplexForCreationDTO, CancellationToken cancellationToken);
        Task<bool> UpdatePlotComplexAsync(Guid id, PlotComplexForUpdateDTO plotComplexForUpdateDTO, CancellationToken cancellationToken);
        Task<bool> DeletePlotComplexAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<PlotComplexDTO>> GetPlotComplexesAsync(CancellationToken cancellationToken);
        Task<PlotComplexDTO> GetPlotComplexByIdAsync(Guid id, CancellationToken cancellationToken);
        #endregion


        #region Plots
        Task<PlotDTO> CreatePlotForPlotComplexAsync(Guid plotComplexId, PlotForCreationDTO plotForCreationDTO, CancellationToken cancellationToken);
        Task<bool> UpdatePlotAsync(Guid plotComplexId, Guid plotId, PlotForUpdateDTO plotForUpdateDTO, CancellationToken cancellationToken);
        Task<bool> DeletePlotAsync(Guid plotComplexId, Guid plotId, CancellationToken cancellationToken);
        Task<IEnumerable<PlotDTO>> GetPlotsByPlotComplexIdAsync(Guid plotComplexId, CancellationToken cancellationToken);
        Task<PlotDTO> GetPlotByIdAsync(Guid plotComplexId, Guid plotId, CancellationToken cancellationToken);        
        #endregion
    }
}
