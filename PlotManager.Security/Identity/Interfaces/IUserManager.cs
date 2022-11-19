using PlotManager.Domain.Identity;
using PlotManager.Security.Identity.Models;
using System.Security.Claims;

namespace PlotManager.Security.Identity.Interfaces
{
    public interface IUserManager<TUser> where TUser : class
    {
        Task<List<string>> GetRolesAsync(TUser user);
        Task<List<Claim>> GetClaimsAsync(TUser user);
        Task<TUser> FindByEmailAsync(string email);
        Task<TUser> FindByNameAsync(string userName);
        Task<UserManagerResult> CreateAsync(TUser user);
    }
}