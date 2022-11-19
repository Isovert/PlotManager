//using Newtonsoft.Json;
//using PlotManager.Contracts;
//using PlotManager.Contracts.Feature;
//using PlotManager.Security.Identity.Models;
//using System.Net.Http.Headers;
//using System.Text;

//namespace PlotManager.UI.Blazor.HttpClients.PlotManagerAPI
//{
//    public class PlotManagerAPIClient : IPlotManagerAPIClient
//    {
//        private readonly HttpClient _httpClient;
//        private string _baseUrl;
//        private Lazy<JsonSerializerSettings> _settings;

//        protected JsonSerializerSettings JsonSerializerSettings { get { return _settings.Value; } }

//        public string BaseUrl
//        {
//            get { return _baseUrl; }
//            set { _baseUrl = value; }
//        }
//        public bool ReadResponseAsString { get; set; }
//        public HttpClient HttpClient { get { return _httpClient; } }

//        public PlotManagerAPIClient(HttpClient httpClient, IConfiguration configuration)
//        {
//            _httpClient = httpClient;
//            _baseUrl = configuration["PlotManagerAPI:BaseURL"];
//            _httpClient.BaseAddress = new Uri(_baseUrl);
//            _settings = new Lazy<JsonSerializerSettings>(CreateSerializerSettings);
//        }

//        private JsonSerializerSettings CreateSerializerSettings()
//        {
//            var settings = new JsonSerializerSettings();
//            return settings;
//        }

//        protected virtual async Task<ObjectResponseResult<T>> ReadObjectResponseAsync<T>(HttpResponseMessage response,
//                                                                                         IReadOnlyDictionary<string,
//                                                                                         IEnumerable<string>> headers,
//                                                                                         CancellationToken cancellationToken)
//        {
//            if (response == null || response.Content == null)
//            {
//                return new ObjectResponseResult<T>(default(T), string.Empty);
//            }

//            if (ReadResponseAsString)
//            {
//                var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
//                try
//                {
//                    var typedBody = JsonConvert.DeserializeObject<T>(responseText, JsonSerializerSettings);
//                    return new ObjectResponseResult<T>(typedBody, responseText);
//                }
//                catch (JsonException exception)
//                {
//                    var message = "Could not deserialize the response body string as " + typeof(T).FullName + ".";
//                    throw new ApiException(message, (int)response.StatusCode, responseText, headers, exception);
//                }
//            }
//            else
//            {
//                try
//                {
//                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
//                    using (var streamReader = new StreamReader(responseStream))
//                    using (var jsonTextReader = new JsonTextReader(streamReader))
//                    {
//                        var serializer = JsonSerializer.Create(JsonSerializerSettings);
//                        var typedBody = serializer.Deserialize<T>(jsonTextReader);
//                        return new ObjectResponseResult<T>(typedBody, string.Empty);
//                    }
//                }
//                catch (JsonException exception)
//                {
//                    var message = "Could not deserialize the response body stream as " + typeof(T).FullName + ".";
//                    throw new ApiException(message, (int)response.StatusCode, string.Empty, headers, exception);
//                }
//            }
//        }

//        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest body, CancellationToken cancellationToken = default)
//        {
//            if (body == null)
//                throw new ArgumentNullException(nameof(body));
//            var urlBuilder_ = new StringBuilder();
//            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/Authentication/authenticate");
//            var client_ = _httpClient;
//            var disposeClient_ = false;
//            try
//            {
//                using (var request_ = new HttpRequestMessage())
//                {
//                    var content_ = new StringContent(JsonConvert.SerializeObject(body));
//                    content_.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
//                    request_.Content = content_;
//                    request_.Method = new HttpMethod("POST");
//                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("text/plain"));
//                    var url_ = urlBuilder_.ToString();
//                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
//                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
//                    var disposeResponse_ = true;
//                    try
//                    {
//                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
//                        if (response_.Content != null && response_.Content.Headers != null)
//                        {
//                            foreach (var item_ in response_.Content.Headers)
//                                headers_[item_.Key] = item_.Value;
//                        }

//                        var status_ = (int)response_.StatusCode;
//                        if (status_ == 200)
//                        {
//                            var objectResponse_ = await ReadObjectResponseAsync<AuthenticationResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
//                            if (objectResponse_.Object == null)
//                            {
//                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
//                            }
//                            return objectResponse_.Object;
//                        }
//                        else
//                        {
//                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
//                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
//                        }
//                    }
//                    finally
//                    {
//                        if (disposeResponse_)
//                            response_.Dispose();
//                    }

//                }
//            }
//            finally
//            {
//                if (disposeClient_)
//                    client_.Dispose();
//            }
//        }

//        public async Task<IEnumerable<FeatureDTO>> GetFeaturesAsync(FeatureResourceParameters featureResourceParameters, CancellationToken cancellationToken)
//        {
//            var urlBuilder_ = new StringBuilder();
//            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/features");
//            var client_ = _httpClient;
//            var disposeClient_ = false;
//            try
//            {
//                using (var request_ = new HttpRequestMessage())
//                {
//                    request_.Method = new HttpMethod("GET");
//                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("text/plain"));
//                    var url_ = urlBuilder_.ToString();
//                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);

//                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
//                    var disposeResponse_ = true;
//                    try
//                    {
//                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
//                        if (response_.Content != null && response_.Content.Headers != null)
//                        {
//                            foreach (var item_ in response_.Content.Headers)
//                                headers_[item_.Key] = item_.Value;
//                        }

//                        var status_ = (int)response_.StatusCode;
//                        if (status_ == 200)
//                        {
//                            var objectResponse_ = await ReadObjectResponseAsync<PagedList<FeatureDTO>>(response_, headers_, cancellationToken).ConfigureAwait(false);
//                            if (objectResponse_.Object == null)
//                            {
//                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
//                            }
//                            return objectResponse_.Object;
//                        }
//                        else
//                        {
//                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
//                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
//                        }
//                    }
//                    finally
//                    {
//                        if (disposeResponse_)
//                            response_.Dispose();
//                    }
//                }
//            }
//            finally
//            {
//                if (disposeClient_)
//                    client_.Dispose();
//            }
//        }

//        public Task<RegistrationResponse> RegisterAsync(RegistrationRequest body, CancellationToken cancellationToken = default)
//        {
//            throw new NotImplementedException();
//        }

//        protected struct ObjectResponseResult<T>
//        {
//            public ObjectResponseResult(T responseObject, string responseText)
//            {
//                this.Object = responseObject;
//                this.Text = responseText;
//            }

//            public T Object { get; }

//            public string Text { get; }
//        }
//    }
//}