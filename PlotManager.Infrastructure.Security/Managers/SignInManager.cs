using PlotManager.Application.Repositories.RepositoryManager;
using PlotManager.Domain.Identity;
using PlotManager.Security.Identity.Interfaces;
using PlotManager.Security.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlotManager.Infrastructure.Security.Managers
{
    public class SignInManager : ISignInManager<User>
    {

        public async Task<SignInResult> PasswordSignInAsync(User user, string password, bool isPersistent, bool lockoutOnFailure)
        {   
            if (user.PasswordHash == password)//TODO ADD HASHING
            {
                return SignInResult.Success;
            }
            else
            {
                return SignInResult.Failed(new List<string>() { "Wrong username or password" });
            }
        }
    }
}
