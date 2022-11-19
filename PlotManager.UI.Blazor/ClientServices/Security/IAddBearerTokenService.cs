using PlotManager.UI.Blazor.HttpClients;

namespace PlotManager.UI.Blazor.ClientServices.Security
{
    public interface IAddBearerTokenService
    {
        Task AddBearerToken(IAPIClientBase client);
    }
}
