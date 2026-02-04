using MediatR;
using Microsoft.AspNetCore.Mvc;
using XTND_Technical_Assessment.Api.Contracts;
using XTND_Technical_Assessment.API.Contracts;
using XTND_Technical_Assessment.Application.Tasks.CreateTask;
using XTND_Technical_Assessment.Application.Tasks.UpdateTask;
using XTND_Technical_Assessment.Application.Tasks.DeleteTask;
using XTND_Technical_Assessment.Application.Tasks.GetTaskById;
using XTND_Technical_Assessment.Application.Tasks.GetPaginatedTasks;
using XTND_Technical_Assessment.Application.Tasks.GetTasksByUser;


namespace XTND_Technical_Assessment.Controllers;


[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator) => _mediator = mediator;


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskRequest body, CancellationToken ct)
    {
        var id = await _mediator.Send(new CreateTaskCommand(body.Title, body.TaskUserId), ct);
        return Created($"/api/tasks/{id}", new { id });
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetTaskByIdQuery(id), ct);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskRequest body, CancellationToken ct)
    {
        var success = await _mediator.Send(new UpdateTaskCommand(id, body.Title, body.TaskStatusId), ct);

        if (!success)
            return NotFound();

        return NoContent();
    }


    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int offset = 0, [FromQuery] int limit = 10, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new GetPaginatedTasksQuery(offset, limit), ct);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var success = await _mediator.Send(new DeleteTaskCommand(id), ct);

        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpGet("user/{userId:int}")]
    public async Task<IActionResult> GetByUser(int userId, [FromQuery] int offset = 0, [FromQuery] int limit = 10, CancellationToken ct = default)
    {
        try
        {
            var result = await _mediator.Send(new GetTasksByUserQuery(userId, offset, limit), ct);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }

}


