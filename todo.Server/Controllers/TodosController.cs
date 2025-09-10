using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
