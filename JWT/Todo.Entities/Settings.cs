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
}