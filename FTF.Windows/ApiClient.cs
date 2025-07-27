using RestSharp;
using System.Security.Policy;

namespace FTF.Windows
{
    public abstract class AbstractHttpClient
    {
        public string BaseUrl { get; set; }
        public string UserAgent { get; set; }

        public string Token { get; set; }

        public AbstractHttpClient(string baseUrl)
        {
            if (!baseUrl.StartsWith("http")) baseUrl = "https://" + baseUrl;
            this.BaseUrl = baseUrl;
        }

        public virtual void SetBaseUrl(string url)
        {
            if (!url.StartsWith("http")) url = "https://" + url;
            BaseUrl = url;
        }

        protected RestClient Client()
        {
            var options = new RestClientOptions(BaseUrl)
            {
                MaxTimeout = 10000,
                Timeout = System.TimeSpan.FromSeconds(7),
            };
            var client = new RestClient(options);


            return client;
        }
    }

    internal class ApiClient : AbstractHttpClient
    {
        private RestClient _client;
        public ApiClient(string baseUrl, string token = "") : base(baseUrl)
        {
            this.AuthToken = token;
            this._client = Client();
            this._client.AddDefaultHeader("Authorization", $"Bearer {AuthToken}");
        }

        public RestResponse Connect()
        {
            var request = new RestRequest("", Method.Get);
            return this._client.Execute(request);
        }

        public RestResponse Authenticate()
        {
            var request = new RestRequest("api/v1/auth-check/client", Method.Post);
            return this._client.Execute(request);
        }

        public RestResponse Submit(AircraftData body)
        {
            var request = new RestRequest("api/v1/position", Method.Post);
            request.AddJsonBody(Newtonsoft.Json.JsonConvert.SerializeObject(body));
            return this._client.Execute(request);
        }

        public string AuthToken
        {
            get => this.Token;
            set => this.Token = value;
        }
    }
}
