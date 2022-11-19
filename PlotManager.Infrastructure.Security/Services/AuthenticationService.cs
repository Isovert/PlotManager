using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PlotManager.Domain.Identity;
using PlotManager.Security.Identity.Interfaces;
using PlotManager.Security.Identity.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PlotManager.Infrastructure.Security.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserManager<User> _userManager;
        private readonly ISignInManager<User> _signInManager;
        private readonly JSONWebTokensSettings _jwtSettings;

        public AuthenticationService(IUserManager<User> userManager,
                                     ISignInManager<User> signInManager,
                                     IOptions<JSONWebTokensSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            User user;
            if (!string.IsNullOrEmpty(request.Username))
            {
                user = await _userManager.FindByNameAsync(request.Username);
            }
            else
            {
                user = await _userManager.FindByEmailAsync(request.Email);
            }
            if(user == null)
            {
                throw new Exception("brak uzytnika"); //TODO FIX
            }
            await _signInManager.PasswordSignInAsync(user, request.Password, false, false);//TODO TAKE VALUES FROM REQUEST

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);
            AuthenticationResponse response = new AuthenticationResponse
            {
                Id = user.Id.ToString(),
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.UserName
            };
            return response;
        }

        public async Task<RegistrationResponse> RegisterAsync(RegistrationRequest request)
        {
            User existingUser;
            existingUser = await _userManager.FindByNameAsync(request.UserName);
            if (existingUser != null)
            {
                throw new Exception("already exist"); //TODO FIX
            }
            existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new Exception("already exist"); //TODO FIX
            }
            var user = new User
            {
                Email = request.Email,
                PasswordHash = request.Password,//TODO ADD HASHING
                UserName = request.UserName,
            };
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                return new RegistrationResponse() { UserId = user.Id.ToString() };
            }
            else
            {
                throw new Exception($"{result.Errors}");
            }
        }

        private async Task<JwtSecurityToken> GenerateToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
            }

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id.ToString())
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}
