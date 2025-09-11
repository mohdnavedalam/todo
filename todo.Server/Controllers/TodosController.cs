using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using todo.Server.Models.Todo;
using todo.Server.Services.Contracts;

namespace todo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoActions _todoActions;
        public TodosController(ITodoActions todoActions)
        {
            _todoActions = todoActions;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTodos()
        {
            var todos = await _todoActions.GetAllTodos();
            return Ok(todos);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetTodoById(int id)
        {
            var todo = await _todoActions.GetTodoById(id);
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
            var updatedTodo = await _todoActions.UpdateTodo(id, todo);
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
            var result = await _todoActions.DeleteTodo(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok("Item deleted.");
        }
    }
}
