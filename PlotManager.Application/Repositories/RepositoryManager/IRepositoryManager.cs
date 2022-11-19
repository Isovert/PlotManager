using PlotManager.Application.Repositories.UnitOfWork;

namespace PlotManager.Application.Repositories.RepositoryManager
{
    public interface IRepositoryManager
    {
        IFeatureRepository FeatureRepository { get; }
        IPlotComplexRepository PlotComplexRepository { get; }
        IPlotRepository PlotRepository { get; }
        IUserRepository UserRepository { get; }
        IUnitOfWork UnitOfWork { get; }
    }
}