using Mapster;
using PlotManager.Application.Repositories.RepositoryManager;
using PlotManager.Application.Services;
using PlotManager.Contracts;
using PlotManager.Contracts.Feature;
using PlotManager.Domain.Entities;
using PlotManager.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlotManager.Application.Services
{
    public class FeatureService : RepositoryServiceBase, IFeatureService
    {
        public FeatureService(IRepositoryManager repositoryManager) : base(repositoryManager)
        {
        }

        public async Task<FeatureDTO> CreateAsync(FeatureForCreationDTO featureForCreationDTO, CancellationToken cancellationToken = default)
        {
            var feature = featureForCreationDTO.Adapt<Feature>();
            RepositoryManager.FeatureRepository.CreateFeature(feature);
            await RepositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
            return feature.Adapt<FeatureDTO>();
        }

        public async Task UpdateAsync(Guid id, FeatureForUpdateDTO featureForUpdateDTO, CancellationToken cancellationToken = default)
        {
            var feature = await RepositoryManager.FeatureRepository.GetFeatureByIdAsync(id, cancellationToken);
            if (feature is null)
            {
                throw new NotFoundException($"Feature with identifier {id} was not found");
            }
            feature.Name = featureForUpdateDTO.Name;
            await RepositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var feature = await RepositoryManager.FeatureRepository.GetFeatureByIdAsync(id, cancellationToken);
            if (feature is null)
            {
                throw new NotFoundException($"Feature with identifier {id} was not found");
            }
            RepositoryManager.FeatureRepository.DeleteFeature(feature);
            await RepositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<FeatureDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var feature = await RepositoryManager.FeatureRepository.GetFeatureByIdAsync(id, cancellationToken);
            if (feature is null)
            {
                throw new NotFoundException($"Feature with identifier {id} was not found");
            }
            return feature.Adapt<FeatureDTO>();
        }

        public async Task<PagedList<FeatureDTO>> GetAsync(FeatureResourceParameters featureResourceParameters, CancellationToken cancellationToken = default)
        {
            var features = await RepositoryManager.FeatureRepository.GetFeaturesAsync(featureResourceParameters, cancellationToken);
            var featuresDtos = new PagedList<FeatureDTO>(features.Select(x => new FeatureDTO() { Id = x.Id, Name = x.Name }).ToList(), features.TotalCount, features.CurrentPage, features.PageSize);
            return featuresDtos;
        }
    }
}