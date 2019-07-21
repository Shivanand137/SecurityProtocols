using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Entities;

namespace Todo.Services
{
    public interface ITodoService
    {
        Task<Response<Todos>> GetTodo(int todoId);
        Task<Response<IEnumerable<Todos>>> GetTodos();
        Task<Response<IEnumerable<Todos>>> GetTodosByUserId(int userId);

        Task<Response<bool>> AddTodo(Todos todo);
        Task<Response<bool>> UpdateTodo(Todos todo);
        Task<Response<bool>> DeleteTodo(int todoId);
    }
}