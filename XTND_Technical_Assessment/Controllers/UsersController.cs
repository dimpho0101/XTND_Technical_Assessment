using MediatR;
using Microsoft.AspNetCore.Mvc;
using XTND_Technical_Assessment.Api.Contracts;
using XTND_Technical_Assessment.API.Contracts;
using XTND_Technical_Assessment.Application.Users.CreateUser;
using XTND_Technical_Assessment.Application.Users.DeleteUser;

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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetUserByIdQuery(id), ct);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        try
        {
            var success = await _mediator.Send(new DeleteUserCommand(id), ct);

            if (!success)
                return NotFound();

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

}

