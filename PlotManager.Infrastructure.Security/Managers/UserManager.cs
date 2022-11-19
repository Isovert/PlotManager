using PlotManager.Application.Repositories.RepositoryManager;
using PlotManager.Domain.Identity;
using PlotManager.Security.Identity.Interfaces;
using PlotManager.Security.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PlotManager.Infrastructure.Security.Managers
{
    public class UserManager : IUserManager<User>
    {
        private readonly IRepositoryManager _repositoryManager;

        public UserManager(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<UserManagerResult> CreateAsync(User user)
        {
            try
            {
                _repositoryManager.UserRepository.CreateUser(user);
                await _repositoryManager.UnitOfWork.SaveChangesAsync();
                return UserManagerResult.Success;
            }
            catch (Exception ex)
            {
                return UserManagerResult.Failed(new List<string> { ex.Message });
            }
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _repositoryManager.UserRepository.GetByUserNameAsync(email);//TODO USE EMAIL
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            return await _repositoryManager.UserRepository.GetByUserNameAsync(userName);
        }

        public async Task<List<Claim>> GetClaimsAsync(User user)
        {
            var c = new Claim("Key", "Value");
            var list = new List<Claim>();
            list.Add(c);
            return await Task.FromResult(list);
        }

        public async Task<List<string>> GetRolesAsync(User user)
        {
            string r = "User";
            var list = new List<string>();
            list.Add(r);
            return await Task.FromResult(list);
        }
    }
}
