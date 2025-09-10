using todo.Server.Models.Todo;

namespace todo.Server.Services.Contracts
{
    public interface ITodoActions
    {
        Task<List<Models.Todo.Todos>> GetAllTodos();
        Task<Todos?> GetTodoById(int id);
        Task<Todos> AddTodo(Todos todo);
        Task<Todos?> UpdateTodo(int id, Todos todo);
        Task<bool> DeleteTodo(int id);
    }
}
