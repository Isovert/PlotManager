using Blazored.LocalStorage;
using PlotManager.UI.Blazor.HttpClients;

namespace PlotManager.UI.Blazor.ClientServices.Security
{
    public class AddBearerTokenService : IAddBearerTokenService
    {
        private readonly ILocalStorageService _localStorageService;

        public AddBearerTokenService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task AddBearerToken(IAPIClientBase client)
        {
            if(await _localStorageService.ContainKeyAsync("token"))
            {
                client.HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _localStorageService.GetItemAsync<string>("token"));
            }    
        }
    }
}
