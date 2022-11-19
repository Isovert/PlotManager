using Microsoft.EntityFrameworkCore;
using PlotManager.Application.Repositories;
using PlotManager.Contracts;
using PlotManager.Contracts.PlotComplex;
using PlotManager.Domain.Entities;
using PlotManager.Infrastructure.Persistance;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlotManager.Infrastructure.Persistance
{
    public class PlotComplexRepository : RepositoryBase<PlotComplex>, IPlotComplexRepository
    {
        public PlotComplexRepository(PlotManagerDbContext plotManagerDbContext) : base(plotManagerDbContext)
        {
        }

        public void CreatePlotComplex(PlotComplex plotComplex)
        {
            base.RepositoryContext.PlotComplexes.Add(plotComplex);
        }
        public void UpdatePlotComplex(PlotComplex plotComplex)
        {
            base.RepositoryContext.PlotComplexes.Update(plotComplex);
        }

        public void DeletePlotComplex(PlotComplex plotComplex)
        {
            base.RepositoryContext.PlotComplexes.Remove(plotComplex);
        }

        public Task<PlotComplex> GetPlotComplexByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return RepositoryContext.PlotComplexes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public Task<PagedList<PlotComplex>> GetPlotComplexesAsync(PlotComplexResourceParameters plotComplexResourceParameters, CancellationToken cancellationToken = default)
        {
            var plotComplexesQueryable = base.RepositoryContext.PlotComplexes.AsQueryable();
            if (!string.IsNullOrEmpty(plotComplexResourceParameters.Name))
            {
                var searchName = plotComplexResourceParameters.Name.Trim();
                plotComplexesQueryable = plotComplexesQueryable.Where(x => x.Name.Contains(searchName));
            }
            var pagedList = PagedListCreator<PlotComplex>.Create(plotComplexesQueryable, plotComplexResourceParameters.PageNumber, plotComplexResourceParameters.PageSize);
            return pagedList;
        }
    }
}