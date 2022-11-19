using PlotManager.Application.Repositories.Base;
using PlotManager.Domain.Identity;

namespace PlotManager.Application.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> GetByUserNameAsync(string username, CancellationToken cancellation = default);
        Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
