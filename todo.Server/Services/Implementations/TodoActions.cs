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
        public async Task<Todos> AddTodo(Todos todo)
        {
            await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public Task<bool> DeleteTodo(int id)
        {
            var todoinDb = _context.Todos.Find(id);
            if (todoinDb == null)
            {
                return Task.FromResult(false);
            }
            _context.Todos.Remove(todoinDb);
            _context.SaveChangesAsync();
            return Task.FromResult(true);
        }

        public Task<List<Todos>> GetAllTodos()
        {
            return _context.Todos.ToListAsync();
        }

        public Task<Todos?> GetTodoById(int id)
        {
            var todoinDb = _context.Todos.Find(id);
            return Task.FromResult(todoinDb);
        }

        public Task<Todos?> UpdateTodo(int id, Todos todo)
        {
            var todoinDb = _context.Todos.Find(id);
            if (todoinDb == null)
            {
                return Task.FromResult<Todos?>(null);
            }
            todoinDb.Task = todo.Task;
            _context.SaveChangesAsync();
            return Task.FromResult<Todos?>(todoinDb);
        }
    }
}
