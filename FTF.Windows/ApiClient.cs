using RestSharp;

namespace FTF.Windows
{
    public class SubmissionBody
    {
        public string callsign { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
    public abstract class AbstractHttpClient
    {
        public string BaseUrl { get; set; }
        public string UserAgent { get; set; }

        public AbstractHttpClient(string baseUrl)
        {
            this.BaseUrl = baseUrl;
        }

        public virtual void SetBaseUrl(string url)
        {
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
        private string password;

        public ApiClient(string baseUrl, string password = "") : base(baseUrl)
        {
            this.password = password;
        }

        public RestResponse Connect()
        {
            var client = Client();
            var request = new RestRequest("", Method.Get);
            return client.Execute(request);
        }

        public RestResponse Authenticate()
        {
            var client = Client();
            var request = new RestRequest("auth", Method.Post);
            request.AddParameter("password", this.password);
            return client.Execute(request);
        }

        public RestResponse Submit(SubmissionBody body)
        {
            var client = Client();
            var request = new RestRequest("api/v1/position", Method.Post);
            request.AddJsonBody(Newtonsoft.Json.JsonConvert.SerializeObject(body));
            return client.Execute(request);
        }
    }
}
