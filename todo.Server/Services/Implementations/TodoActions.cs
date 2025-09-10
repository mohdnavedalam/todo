using todo.Server.Models.Todo;
using todo.Server.Services.Contracts;

namespace todo.Server.Services.Implementations
{
    public class TodoActions : ITodoActions
    {
        public Task<Todos> AddTodo(Todos todo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTodo(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Todos>> GetAllTodos()
        {
            throw new NotImplementedException();
        }

        public Task<Todos?> GetTodoById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Todos?> UpdateTodo(int id, Todos todo)
        {
            throw new NotImplementedException();
        }
    }
}
