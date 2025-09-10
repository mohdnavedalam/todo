using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using todo.Server.Data;
using todo.Server.Models.Todo;
using todo.Server.Services.Contracts;

namespace todo.Server.Services.Implementations
{
    public class TodoActions : ITodoActions
    {
        private AppDbContext _context;
        public TodoActions(AppDbContext context)
        {
            _context = context;
        }
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
            return _context.Todos.ToListAsync();
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
