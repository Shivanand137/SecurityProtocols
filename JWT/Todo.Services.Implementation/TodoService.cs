using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Todo.Entities;

namespace Todo.Services.Implementation
{
    public class TodoService : ITodoService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TodoService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Response<Todos>> GetTodo(int todoId)
        {
            using (var _client = _httpClientFactory.CreateClient(HttpClientSettings.HttpClientName))
            {
                var todoString = await _client.GetStringAsync($"{ApiEndpoints.Todos}/{todoId}");
                var todo = JsonConvert.DeserializeObject<Todos>(todoString);
                return Response<Todos>.Success(todo);
            }
        }

        public async Task<Response<IEnumerable<Todos>>> GetTodos()
        {
            using (var _client = _httpClientFactory.CreateClient(HttpClientSettings.HttpClientName))
            {
                var todoString = await _client.GetStringAsync(ApiEndpoints.Todos);
                var todos = JsonConvert.DeserializeObject<List<Todos>>(todoString);
                return Response<IEnumerable<Todos>>.Success(todos);
            }
        }

        public async Task<Response<IEnumerable<Todos>>> GetTodosByUserId(int userId)
        {
            using (var _client = _httpClientFactory.CreateClient(HttpClientSettings.HttpClientName))
            {
                var todoString = await _client.GetStringAsync($"{ApiEndpoints.Todos}?userId={userId}");
                var todos = JsonConvert.DeserializeObject<List<Todos>>(todoString);
                return Response<IEnumerable<Todos>>.Success(todos);
            }
        }

        public async Task<Response<bool>> AddTodo(Todos todo)
        {
            using (var _client = _httpClientFactory.CreateClient(HttpClientSettings.HttpClientName))
            {
                var jsonObject = JsonConvert.SerializeObject(todo);
                var stringContent = new StringContent(jsonObject, HttpClientSettings.Encoding, HttpClientSettings.MediaType);
                var response = await _client.PostAsync(ApiEndpoints.Todos, stringContent);
                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                    return Response<bool>.Success("Todo item added successfully!");
            }
            return Response<bool>.Failure("Could not add todo item.");
        }

        public async Task<Response<bool>> DeleteTodo(int todoId)
        {
            using (var _client = _httpClientFactory.CreateClient(HttpClientSettings.HttpClientName))
            {
                var response = await _client.DeleteAsync($"{ApiEndpoints.Todos}/{todoId}");
                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                    return Response<bool>.Success("Todo item deleted successfully!");
            }
            return Response<bool>.Failure("Could not delete todo item.");
        }

        public async Task<Response<bool>> UpdateTodo(Todos todo)
        {
            using (var _client = _httpClientFactory.CreateClient(HttpClientSettings.HttpClientName))
            {
                var jsonObject = JsonConvert.SerializeObject(todo);
                var stringContent = new StringContent(jsonObject, HttpClientSettings.Encoding, HttpClientSettings.MediaType);
                var response = await _client.PutAsync($"{ApiEndpoints.Todos}/{todo.Id}", stringContent);
                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                    return Response<bool>.Success("Todo item updated successfully!");
            }
            return Response<bool>.Failure("Could not update todo item.");
        }
    }
}
