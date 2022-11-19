using PlotManager.Application.Repositories.Base;
using PlotManager.Contracts;
using PlotManager.Contracts.Plot;
using PlotManager.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlotManager.Application.Repositories
{
    public interface IPlotRepository : IRepositoryBase<Plot>
    {
        Task<Plot> GetPlotByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<PagedList<Plot>> GetPlotsAsync(Guid plotComplexId, PlotResourceParameters plotResourceParameters, CancellationToken cancellationToken = default);
        void CreatePlot(Plot plot);
        void UpdatePlot(Plot plot);
        void DeletePlot(Plot plot);
    }
}