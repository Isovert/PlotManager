using Microsoft.EntityFrameworkCore;
using PlotManager.Application.Repositories;
using PlotManager.Contracts;
using PlotManager.Contracts.Feature;
using PlotManager.Domain.Entities;
using PlotManager.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlotManager.Infrastructure.Persistance
{
    public class FeatureRepository : RepositoryBase<Feature>, IFeatureRepository
    {
        public FeatureRepository(PlotManagerDbContext plotManagerDbContext) : base(plotManagerDbContext)
        {
        }

        public void CreateFeature(Feature feature)
        {
            base.Create(feature);
        }
        public void UpdateFeature(Feature feature)
        {
            base.Update(feature);
        }

        public void DeleteFeature(Feature feature)
        {
            base.Delete(feature);
        }

        public Task<List<Feature>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return RepositoryContext.Features.AsNoTracking().ToListAsync(cancellationToken);
        }

        public Task<Feature> GetFeatureByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return RepositoryContext.Features.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public Task<PagedList<Feature>> GetFeaturesAsync(FeatureResourceParameters featureResourceParameters, CancellationToken cancellationToken = default)
        {
            var featuresQueryable = base.RepositoryContext.Features.AsQueryable();
            if (!string.IsNullOrEmpty(featureResourceParameters.Name))
            {
                var searchName = featureResourceParameters.Name.Trim();
                featuresQueryable = featuresQueryable.Where(x => x.Name.Contains(searchName));
            }
            var pagedList = PagedListCreator<Feature>.Create(featuresQueryable, featureResourceParameters.PageNumber, featureResourceParameters.PageSize);
            return pagedList;
        }
    }
}