using PlotManager.Application.Repositories.Base;
using PlotManager.Contracts;
using PlotManager.Contracts.Feature;
using PlotManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PlotManager.Application.Repositories
{
    public interface IFeatureRepository : IRepositoryBase<Feature>
    {
        Task<Feature> GetFeatureByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<PagedList<Feature>> GetFeaturesAsync(FeatureResourceParameters featureResourceParameters, CancellationToken cancellationToken = default);
        Task<List<Feature>> GetAllAsync(CancellationToken cancellationToken = default);
        void CreateFeature(Feature feature);
        void UpdateFeature(Feature feature);
        void DeleteFeature(Feature feature);
    }
}