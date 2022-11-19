using PlotManager.Application.Repositories;
using PlotManager.Application.Repositories.RepositoryManager;
using PlotManager.Application.Repositories.UnitOfWork;
using PlotManager.Persistence.Repository;

namespace PlotManager.Infrastructure.Persistance
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IFeatureRepository> _featureRepository;
        private readonly Lazy<IPlotRepository> _plotRepository;
        private readonly Lazy<IPlotComplexRepository> _plotComplexRepository;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IUnitOfWork> _unitOfWork;

        public RepositoryManager(PlotManagerDbContext plotManagerDbContext)
        {
            _featureRepository = new Lazy<IFeatureRepository>(() => new FeatureRepository(plotManagerDbContext));
            _plotRepository = new Lazy<IPlotRepository>(() => new PlotRepository(plotManagerDbContext));
            _plotComplexRepository = new Lazy<IPlotComplexRepository>(() => new PlotComplexRepository(plotManagerDbContext));
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(plotManagerDbContext));
            _unitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(plotManagerDbContext));
        }

        public IFeatureRepository FeatureRepository => _featureRepository.Value;
        public IPlotRepository PlotRepository => _plotRepository.Value;
        public IPlotComplexRepository PlotComplexRepository => _plotComplexRepository.Value;
        public IUserRepository UserRepository => _userRepository.Value;
        public IUnitOfWork UnitOfWork => _unitOfWork.Value;
    }
}