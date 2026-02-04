using MediatR;
using Microsoft.EntityFrameworkCore;
using XTND_Technical_Assessment.Infra;
using XTND_Technical_Assessment.API.Contracts;
using XTND_Technical_Assessment.Infrastructure.Logging;

namespace XTND_Technical_Assessment.Application.Tasks.GetTaskById;

public sealed class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, GetTaskByIdResponse?>
{
    private readonly Infra.AppDbContext _db;
    private readonly ILogger<GetTaskByIdHandler> _logger;

    public GetTaskByIdHandler(Infra.AppDbContext db, ILogger<GetTaskByIdHandler> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<GetTaskByIdResponse?> Handle(GetTaskByIdQuery request, CancellationToken ct)
    {
        try
        {
            var result = await _db.TaskItems
                .Include(t => t.TaskUser)
                .Include(t => t.TaskStatus)
                .Where(t => t.Id == request.Id)
                .Select(t => new GetTaskByIdResponse
                {
                    Id = t.Id,
                    Title = t.Title,
                    TaskUserId = t.TaskUserId,
                    UserDisplayName = t.TaskUser != null ? t.TaskUser.DisplayName : "",
                    TaskStatusId = t.TaskStatusId,
                    StatusName = t.TaskStatus != null ? t.TaskStatus.Name : "",
                    TaskCreatedAtUtc = t.TaskCreatedAtUtc,
                    TaskUpdatedAtUtc = t.TaskUpdatedAtUtc
                })
                .FirstOrDefaultAsync(ct);

            if (result is null)
            {
                _logger.LogNotFound("Task", request.Id);
            }
            else
            {
                _logger.LogQuery("GetTaskById");
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogQueryError("GetTaskById", ex);
            throw;
        }
    }
}
