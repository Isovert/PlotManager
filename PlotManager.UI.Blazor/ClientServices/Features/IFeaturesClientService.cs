using PlotManager.Contracts;
using PlotManager.Contracts.Feature;

namespace PlotManager.UI.Blazor.ClientServices.Features
{
    public interface IFeaturesClientService
    {
        Task<List<FeatureDTO>> GetFeaturesAsync(FeatureResourceParameters featureResourceParameters, CancellationToken cancellationToken = default);
        Task<FeatureDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<FeatureDTO> CreateAsync(FeatureForCreationDTO featureForCreationDTO, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Guid id, FeatureForUpdateDTO featureForUpdateDTO, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}