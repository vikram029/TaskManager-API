using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Models;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly AppDbContext _db;

    public TasksController(AppDbContext db) => _db = db;

    // GET /api/tasks?completed=true
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetAll([FromQuery] bool? completed)
    {
        var query = _db.Tasks.AsNoTracking();

        if (completed.HasValue)
            query = query.Where(t => t.IsCompleted == completed.Value);

        return await query.OrderByDescending(t => t.CreatedAt).ToListAsync();
    }

    // GET /api/tasks/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TaskItem>> GetById(int id)
    {
        var task = await _db.Tasks.FindAsync(id);
        return task is null ? NotFound() : task;
    }

    // POST /api/tasks
    [HttpPost]
    public async Task<ActionResult<TaskItem>> Create(TaskInput input)
    {
        var task = new TaskItem
        {
            Title = input.Title,
            Description = input.Description,
            IsCompleted = input.IsCompleted,
            DueDate = input.DueDate
        };

        _db.Tasks.Add(task);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    // PUT /api/tasks/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, TaskInput input)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task is null) return NotFound();

        task.Title = input.Title;
        task.Description = input.Description;
        task.IsCompleted = input.IsCompleted;
        task.DueDate = input.DueDate;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    // DELETE /api/tasks/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task is null) return NotFound();

        _db.Tasks.Remove(task);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
