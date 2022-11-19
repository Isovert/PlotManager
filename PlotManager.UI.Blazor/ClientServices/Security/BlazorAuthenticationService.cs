using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using PlotManager.Security.Identity.Models;
using PlotManager.UI.Blazor.HttpClients.PlotManagerAPI;
using System.Net.Http.Headers;

namespace PlotManager.UI.Blazor.ClientServices.Security
{
    public class BlazorAuthenticationService : IBlazorAuthenticationService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        protected readonly ILocalStorageService _localStorage;

        protected IPlotManagerAPIClient _client;

        public BlazorAuthenticationService(IPlotManagerAPIClient client,
                                           ILocalStorageService localStorage,
                                           AuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _client = client;
            _localStorage = localStorage;
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            try
            {
                AuthenticationRequest authenticationRequest = new AuthenticationRequest() { Email = email, Password = password };
                var authenticationResponse = await _client.AuthenticateAsync(authenticationRequest);
                if (authenticationResponse.Token != string.Empty)
                {
                    await _localStorage.SetItemAsync("token", authenticationResponse.Token);
                    ((CustomAuthenticationStateProvider)_authenticationStateProvider).SetUserAuthenticated(email);
                    _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authenticationResponse.Token);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Register(string firstName, string lastName, string userName, string email, string password)
        {
            RegistrationRequest registrationRequest = new RegistrationRequest() { FirstName = firstName, LastName = lastName, Email = email, UserName = userName, Password = password };
            var response = await _client.RegisterAsync(registrationRequest);

            if (!string.IsNullOrEmpty(response.UserId))
            {
                return true;
            }
            return false;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("token");
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).SetUserLoggedOut();
            _client.HttpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}