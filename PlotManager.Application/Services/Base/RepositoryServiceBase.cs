using PlotManager.Application.Repositories.RepositoryManager;

namespace PlotManager.Application.Services
{
    public abstract class RepositoryServiceBase
    {
        protected IRepositoryManager RepositoryManager { get; private set; }

        protected RepositoryServiceBase(IRepositoryManager repositoryManager)
        {
            RepositoryManager = repositoryManager;
        }
    }
}