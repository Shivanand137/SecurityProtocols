using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Todo.Entities;
using Todo.Services;

namespace Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodosController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        [Route("{todoId}")]
        public async Task<IActionResult> GetTodo(int todoId)
        {
            var todo = await _todoService.GetTodo(todoId);
            return Ok(todo);
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            var todos = await _todoService.GetTodos();
            return Ok(todos);
        }

        [HttpGet]
        [Route("users/{userId}")]
        public async Task<IActionResult> GetTodosByUser(int userId)
        {
            var todos = await _todoService.GetTodosByUserId(userId);
            return Ok(todos);
        }

        [HttpPost]
        public async Task<IActionResult> AddTodo(Todos todo)
        {
            var response = await _todoService.AddTodo(todo);
            return response.IsSuccess ? Ok(response.Data) : (IActionResult)BadRequest(response.Data);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTodo(Todos todo)
        {
            var response = await _todoService.UpdateTodo(todo);
            return response.IsSuccess ? Ok(response.Data) : (IActionResult)BadRequest(response.Data);
        }

        [HttpDelete]
        [Route("{todoId}")]
        public async Task<IActionResult> DeleteTodo(int todoId)
        {
            var response = await _todoService.DeleteTodo(todoId);
            return response.IsSuccess ? Ok(response.Data) : (IActionResult)BadRequest(response.Data);
        }
    }
}