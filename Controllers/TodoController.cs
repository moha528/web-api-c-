using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/todo")]
[Authorize]
public class TodoController : ControllerBase
{
    private readonly TodoService _todoService;

    public TodoController(TodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public async Task<ActionResult<List<TodoItem>>> GetAll()
    {
        var todos = await _todoService.GetAllAsync();
        return Ok(todos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItem>> GetById(string id)
    {
        var todo = await _todoService.GetByIdAsync(id);
        if (todo is null)
            return NotFound();
        return Ok(todo);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<TodoItem>> Create(TodoItem newTodo)
    {
        await _todoService.CreateAsync(newTodo);
        return CreatedAtAction(nameof(GetById), new { id = newTodo.Id }, newTodo);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(string id, TodoItem updatedTodo)
    {
        var todo = await _todoService.GetByIdAsync(id);
        if (todo is null)
            return NotFound();

        updatedTodo.Id = todo.Id;
        await _todoService.UpdateAsync(id, updatedTodo);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(string id)
    {
        var todo = await _todoService.GetByIdAsync(id);
        if (todo is null)
            return NotFound();

        await _todoService.DeleteAsync(id);
        return NoContent();
    }
}
