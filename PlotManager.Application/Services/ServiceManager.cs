using PlotManager.Application.Repositories.RepositoryManager;

namespace PlotManager.Application.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IFeatureService> _featureService;
        private readonly Lazy<IPlotComplexService> _plotComplexService;
        private readonly Lazy<IPlotService> _plotService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _featureService = new Lazy<IFeatureService>(() => new FeatureService(repositoryManager));
            _plotComplexService = new Lazy<IPlotComplexService>(() => new PlotComplexService(repositoryManager));
            _plotService = new Lazy<IPlotService>(() => new PlotService(repositoryManager));
        }

        public IFeatureService FeatureService => _featureService.Value;
        public IPlotComplexService PlotComplexService => _plotComplexService.Value;
        public IPlotService PlotService => _plotService.Value;
    }
}