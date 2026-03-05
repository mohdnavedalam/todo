using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using todo.Server.Models.Todo;
using todo.Server.Services.Contracts;

namespace todo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodosController : ControllerBase
    {
        private readonly ITodoActions _todoActions;
        public TodosController(ITodoActions todoActions)
        {
            _todoActions = todoActions;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTodos()
        {
            var userId = GetUserId();
            var todos = await _todoActions.GetAllTodos(userId);
            return Ok(todos);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetTodoById(int id)
        {
            var userId = GetUserId();
            var todo = await _todoActions.GetTodoById(id, userId);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> AddTodo([FromBody] Todos todo)
        {
            if (todo == null || string.IsNullOrWhiteSpace(todo.Task))
            {
                return BadRequest("Invalid todo item.");
            }
            todo.UserId = GetUserId();
            var createdTodo = await _todoActions.AddTodo(todo);
            return CreatedAtAction(nameof(GetTodoById), new { id = createdTodo.Id }, createdTodo);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateTodo(int id, [FromBody] Todos todo)
        {
            if (todo == null || string.IsNullOrWhiteSpace(todo.Task))
            {
                return BadRequest("Invalid todo item.");
            }
            var userId = GetUserId();
            var updatedTodo = await _todoActions.UpdateTodo(id, todo, userId);
            if (updatedTodo == null)
            {
                return NotFound();
            }
            return Ok(updatedTodo);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var userId = GetUserId();
            var result = await _todoActions.DeleteTodo(id, userId);
            if (!result)
            {
                return NotFound();
            }
            return Ok("Item deleted.");
        }
    }
}
