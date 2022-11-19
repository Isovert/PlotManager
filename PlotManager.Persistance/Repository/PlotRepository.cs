using Microsoft.EntityFrameworkCore;
using PlotManager.Application.Repositories;
using PlotManager.Contracts;
using PlotManager.Contracts.Plot;
using PlotManager.Domain.Entities;
using PlotManager.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace PlotManager.Infrastructure.Persistance
{
    public class PlotRepository : RepositoryBase<Plot>, IPlotRepository
    {
        public PlotRepository(PlotManagerDbContext plotManagerDbContext) : base(plotManagerDbContext)
        {
        }

        public void CreatePlot(Plot plot)
        {
            base.RepositoryContext.Plots.Add(plot);
        }

        public void UpdatePlot(Plot plot)
        {
            base.RepositoryContext.Plots.Update(plot);
        }

        public void DeletePlot(Plot plot)
        {
            base.RepositoryContext.Plots.Remove(plot);
        }

        public Task<Plot> GetPlotByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return base.RepositoryContext.Plots.Include(x => x.PlotFeatures)
                                               .ThenInclude(x => x.Feature)
                                               .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public Task<PagedList<Plot>> GetPlotsAsync(Guid plotComplexId, PlotResourceParameters plotResourceParameters, CancellationToken cancellationToken = default)
        {
            var plotsQueryable = base.RepositoryContext.Plots.AsQueryable();
            plotsQueryable = plotsQueryable.Where(x => x.PlotComplexId == plotComplexId);
            var pagedList = PagedListCreator<Plot>.Create(plotsQueryable, plotResourceParameters.PageNumber, plotResourceParameters.PageSize);
            return pagedList;
        }
    }
}