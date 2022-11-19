using PlotManager.Domain.Identity;
using PlotManager.Security.Identity.Models;

namespace PlotManager.Security.Identity.Interfaces
{
    public interface ISignInManager<TUser> where TUser : class
    {
        Task<SignInResult> PasswordSignInAsync(User user, string password, bool isPersistent, bool lockoutOnFailure);
    }
}