using MediatR;
using Microsoft.AspNetCore.Mvc;
using XTND_Technical_Assessment.Api.Contracts;
using XTND_Technical_Assessment.Application.Users.CreateUser;

namespace XTND_Technical_Assessment.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest body, CancellationToken ct)
    {
        var id = await _mediator.Send(new CreateUserCommand(body.DisplayName), ct);
        return Created($"/api/users/{id}", new { id });
    }
}
