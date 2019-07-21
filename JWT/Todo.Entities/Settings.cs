using Newtonsoft.Json;
using System.Text;

namespace Todo.Entities
{
    public class HttpClientSettings
    {
        public const string HttpClientName = "TodoJsonClient";
        public const string BaseUrl = "https://jsonplaceholder.typicode.com";
        public const string MediaType = "application/json";
        public static Encoding Encoding = Encoding.UTF8;
    }

    public class ApiEndpoints
    {
        public const string Todos = "todos";
    }

    [JsonObject("secretSettings")]
    public class SecretManagement
    {
        [JsonProperty("tokenSecret")]
        public string TokenSecret { get; set; }
    }

    [JsonObject("tokenManagement")]
    public class TokenManagement
    {
        [JsonProperty("issuer")]
        public string Issuer { get; set; }

        [JsonProperty("audience")]
        public string Audience { get; set; }

        [JsonProperty("accessExpiration")]
        public string AccessExpiration { get; set; }

        [JsonProperty("refreshExpiration")]
        public string RefreshExpiration { get; set; }
    }
}