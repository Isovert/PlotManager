using PlotManager.Application.Repositories.UnitOfWork;
using PlotManager.Infrastructure.Persistance;
using PlotManager.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace PlotManager.Infrastructure.Persistance
{
    internal class UnitOfWork : IUnitOfWork
    {
        private PlotManagerDbContext _plotManagerDbContext;

        public UnitOfWork(PlotManagerDbContext plotManagerDbContext)
        {
            _plotManagerDbContext = plotManagerDbContext;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _plotManagerDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}