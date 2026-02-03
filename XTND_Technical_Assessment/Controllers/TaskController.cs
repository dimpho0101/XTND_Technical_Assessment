using MediatR;
using Microsoft.AspNetCore.Mvc;
using XTND_Technical_Assessment.Api.Contracts;
using XTND_Technical_Assessment.Application.Tasks.CreateTask;

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
}
