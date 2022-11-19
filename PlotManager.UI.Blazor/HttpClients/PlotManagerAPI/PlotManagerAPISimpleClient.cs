using Newtonsoft.Json;
using PlotManager.Contracts.Feature;
using PlotManager.Contracts.Plot;
using PlotManager.Contracts.PlotComplex;
using PlotManager.Security.Identity.Models;
using System.Text;
using System.Web;

namespace PlotManager.UI.Blazor.HttpClients.PlotManagerAPI
{
    public class PlotManagerAPISimpleClient : IPlotManagerAPIClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public string BaseUrl
        {
            get { return _baseUrl; }
        }

        public HttpClient HttpClient { get { return _httpClient; } }

        public PlotManagerAPISimpleClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["PlotManagerAPI:BaseURL"];
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest body, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Authentication/authenticate", body, cancellationToken);
            var jsonString = response.Content.ReadAsStringAsync();
            jsonString.Wait();
            return JsonConvert.DeserializeObject<AuthenticationResponse>(jsonString.Result);
        }

        public Task<RegistrationResponse> RegisterAsync(RegistrationRequest body, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        #region Features
        public async Task<IEnumerable<FeatureDTO>> GetFeaturesAsync(FeatureResourceParameters featureResourceParameters, CancellationToken cancellationToken = default)
        {
            var builder = new UriBuilder(_baseUrl + "/api/features");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query[nameof(featureResourceParameters.Name)] = featureResourceParameters.Name;
            builder.Query = query.ToString();
            var response = await _httpClient.GetAsync(builder.Uri, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<FeatureDTO>>(jsonString);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new List<FeatureDTO>();
            }
            throw new Exception("Error");//TODO FIX
        }

        public async Task<FeatureDTO> GetFeatureByID(Guid id, CancellationToken cancellationToken)
        {
            var builder = new UriBuilder(_baseUrl + "/api/features/" + id);
            return await _httpClient.GetFromJsonAsync<FeatureDTO>(builder.Uri);
        }

        public async Task<FeatureDTO> CreateFeatureAsync(FeatureForCreationDTO featureForCreationDTO, CancellationToken cancellationToken)
        {
            var builder = new UriBuilder(_baseUrl + "/api/features/");
            var response = await _httpClient.PostAsJsonAsync(builder.Uri, featureForCreationDTO, cancellationToken);
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<FeatureDTO>(jsonString);
        }

        public async Task<bool> UpdateFeatureAsync(Guid id, FeatureForUpdateDTO featureForUpdateDTO, CancellationToken cancellationToken)
        {
            var builder = new UriBuilder(_baseUrl + "/api/features/" + id);
            var requestContent = new StringContent(JsonConvert.SerializeObject(featureForUpdateDTO), Encoding.UTF8, "application/json-patch+json");
            var response = await _httpClient.PatchAsync(builder.Uri, requestContent, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteFeatureAsync(Guid id, CancellationToken cancellationToken)
        {
            var builder = new UriBuilder(_baseUrl + "/api/features/" + id);
            var response = await _httpClient.DeleteAsync(builder.Uri, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region PlotComplexes
        public async Task<PlotComplexDTO> CreatePlotComplexAsync(PlotComplexForCreationDTO plotComplexForCreationDTO, CancellationToken cancellationToken)
        {
            var builder = new UriBuilder(_baseUrl + "/api/plotcomplexes/");
            var response = await _httpClient.PostAsJsonAsync(builder.Uri, plotComplexForCreationDTO, cancellationToken);
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PlotComplexDTO>(jsonString);
        }

        public async Task<bool> UpdatePlotComplexAsync(Guid id, PlotComplexForUpdateDTO plotComplexForUpdateDTO, CancellationToken cancellationToken)
        {
            var builder = new UriBuilder(_baseUrl + "/api/plotComplexes/" + id);
            var requestContent = new StringContent(JsonConvert.SerializeObject(plotComplexForUpdateDTO), Encoding.UTF8, "application/json-patch+json");
            var response = await _httpClient.PatchAsync(builder.Uri, requestContent, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeletePlotComplexAsync(Guid id, CancellationToken cancellationToken)
        {
            var builder = new UriBuilder(_baseUrl + "/api/plotcomplexes/" + id);
            var response = await _httpClient.DeleteAsync(builder.Uri, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<PlotComplexDTO>> GetPlotComplexesAsync(CancellationToken cancellationToken)
        {
            var builder = new UriBuilder(_baseUrl + "/api/plotComplexes");
            var response = await _httpClient.GetAsync(builder.Uri, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<PlotComplexDTO>>(jsonString);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new List<PlotComplexDTO>();
            }
            throw new Exception("Error");//TODO FIX
        }

        public async Task<PlotComplexDTO> GetPlotComplexByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var builder = new UriBuilder(_baseUrl + "/api/plotComplexes/" + id);
            return await _httpClient.GetFromJsonAsync<PlotComplexDTO>(builder.Uri);
        }
        #endregion

        #region Plots
        public async Task<PlotDTO> CreatePlotForPlotComplexAsync(Guid plotComplexId, PlotForCreationDTO plotForCreationDTO, CancellationToken cancellationToken)
        {
            var builder = new UriBuilder(_baseUrl + $"/api/plotcomplexes/{plotComplexId}/plots");
            var response = await _httpClient.PostAsJsonAsync(builder.Uri, plotForCreationDTO, cancellationToken);
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PlotDTO>(jsonString);
        }

        public async Task<bool> UpdatePlotAsync(Guid plotComplexId, Guid plotId, PlotForUpdateDTO plotForUpdateDTO, CancellationToken cancellationToken)
        {
            var builder = new UriBuilder(_baseUrl + $"/api/plotcomplexes/{plotComplexId}/plots/{plotId}");
            var requestContent = new StringContent(JsonConvert.SerializeObject(plotForUpdateDTO), Encoding.UTF8, "application/json-patch+json");
            var response = await _httpClient.PatchAsync(builder.Uri, requestContent, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeletePlotAsync(Guid plotComplexId, Guid plotId, CancellationToken cancellationToken)
        {
            var builder = new UriBuilder(_baseUrl + $"/api/plotcomplexes/{plotComplexId}/plots/{plotId}");
            var response = await _httpClient.DeleteAsync(builder.Uri, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<PlotDTO> GetPlotByIdAsync(Guid plotComplexId, Guid plotId, CancellationToken cancellationToken)
        {
            var builder = new UriBuilder(_baseUrl + $"/api/plotComplexes/{plotComplexId}/plots/{plotId}");
            return await _httpClient.GetFromJsonAsync<PlotDTO>(builder.Uri);
        }

        public async Task<IEnumerable<PlotDTO>> GetPlotsByPlotComplexIdAsync(Guid plotComplexId, CancellationToken cancellationToken)
        {
            var builder = new UriBuilder(_baseUrl + $"/api/plotcomplexes/{plotComplexId}/plots");
            var response = await _httpClient.GetAsync(builder.Uri, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<PlotDTO>>(jsonString);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new List<PlotDTO>();
            }
            throw new Exception("Error");//TODO FIX
        }
        #endregion
    }
}