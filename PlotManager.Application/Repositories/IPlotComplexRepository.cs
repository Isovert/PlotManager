using PlotManager.Contracts;
using PlotManager.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using PlotManager.Contracts.PlotComplex;
using PlotManager.Application.Repositories.Base;

namespace PlotManager.Application.Repositories
{
    public interface IPlotComplexRepository : IRepositoryBase<PlotComplex>
    {
        Task<PlotComplex> GetPlotComplexByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<PagedList<PlotComplex>> GetPlotComplexesAsync(PlotComplexResourceParameters plotComplexResourceParameters, CancellationToken cancellationToken = default);
        void CreatePlotComplex(PlotComplex plotComplex);
        void UpdatePlotComplex(PlotComplex plotComplex);
        void DeletePlotComplex(PlotComplex plotComplex);
    }
}