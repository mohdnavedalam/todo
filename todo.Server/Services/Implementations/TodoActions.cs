using Microsoft.EntityFrameworkCore;
using todo.Server.Data;
using todo.Server.Models.Todo;
using todo.Server.Services.Contracts;

namespace todo.Server.Services.Implementations
{
    public class TodoActions : ITodoActions
    {
        private readonly AppDbContext _context;
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

        public async Task<bool> DeleteTodo(int id, int userId)
        {
            var todoinDb = await _context.Todos.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (todoinDb == null)
            {
                return false;
            }
            _context.Todos.Remove(todoinDb);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Todos>> GetAllTodos(int userId)
        {
            return await _context.Todos.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<Todos?> GetTodoById(int id, int userId)
        {
            return await _context.Todos.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        }

        public async Task<Todos?> UpdateTodo(int id, Todos todo, int userId)
        {
            var todoinDb = await _context.Todos.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (todoinDb == null)
            {
                return null;
            }
            todoinDb.Task = todo.Task;
            await _context.SaveChangesAsync();
            return todoinDb;
        }
    }
}
