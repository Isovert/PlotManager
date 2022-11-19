using Microsoft.EntityFrameworkCore;
using PlotManager.Application.Repositories;
using PlotManager.Domain.Identity;
using PlotManager.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PlotManager.Persistence.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(PlotManagerDbContext plotManagerDbContext) : base(plotManagerDbContext)
        {
        }

        public Task<User> GetByUserNameAsync(string username, CancellationToken cancellation = default)
        {
            return base.RepositoryContext.Users.FirstOrDefaultAsync(x => x.UserName == username);
        }

        public Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return base.RepositoryContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void CreateUser(User user)
        {
            base.RepositoryContext.Users.Add(user);
        }

        public void UpdateUser(User user)
        {
            base.RepositoryContext.Users.Update(user);
        }

        public void DeleteUser(User user)
        {
            base.RepositoryContext.Users.Remove(user);
        }
    }
}
