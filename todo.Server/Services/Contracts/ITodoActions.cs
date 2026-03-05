using todo.Server.Models.Todo;

namespace todo.Server.Services.Contracts
{
    public interface ITodoActions
    {
        Task<List<Todos>> GetAllTodos(int userId);
        Task<Todos?> GetTodoById(int id, int userId);
        Task<Todos> AddTodo(Todos todo);
        Task<Todos?> UpdateTodo(int id, Todos todo, int userId);
        Task<bool> DeleteTodo(int id, int userId);
    }
}
