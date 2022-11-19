using PlotManager.Contracts;
using PlotManager.Contracts.Feature;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlotManager.Application.Services
{
    public interface IFeatureService
    {
        Task<FeatureDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<PagedList<FeatureDTO>> GetAsync(FeatureResourceParameters featureResourceParameters, CancellationToken cancellationToken = default);
        Task<FeatureDTO> CreateAsync(FeatureForCreationDTO featureForCreationDTO, CancellationToken cancellationToken = default);
        Task UpdateAsync(Guid id, FeatureForUpdateDTO featureForUpdateDTO, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}