using MediatR;
using Microsoft.EntityFrameworkCore;
using XTND_Technical_Assessment.Infra;
using XTND_Technical_Assessment.API.Contracts;
using XTND_Technical_Assessment.Infrastructure.Logging;

namespace XTND_Technical_Assessment.Application.Tasks.GetTasksByUser;

public sealed class GetTasksByUserHandler : IRequestHandler<GetTasksByUserQuery, PaginatedUserTasksResponse>
{
    private readonly AppDbContext _db;
    private readonly ILogger<GetTasksByUserHandler> _logger;

    public GetTasksByUserHandler(AppDbContext db, ILogger<GetTasksByUserHandler> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<PaginatedUserTasksResponse> Handle(GetTasksByUserQuery request, CancellationToken ct)
    {
        try
        {
            var offset = Math.Max(0, request.Offset);
            var limit = Math.Clamp(request.Limit, 1, 100); 

            var user = await _db.TaskUsers.FirstOrDefaultAsync(u => u.Id == request.UserId, ct);
            if (user is null)
            {
                _logger.LogNotFound("User", request.UserId);
                throw new InvalidOperationException($"User with ID {request.UserId} not found.");
            }

            var totalCount = await _db.TaskItems
                .CountAsync(t => t.TaskUserId == request.UserId, ct);

            var tasks = await _db.TaskItems
                .Include(t => t.TaskUser)
                .Include(t => t.TaskStatus)
                .Where(t => t.TaskUserId == request.UserId)
                .OrderByDescending(t => t.TaskCreatedAtUtc)
                .Skip(offset)
                .Take(limit)
                .Select(t => new TaskSummary
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
                .ToListAsync(ct);

            var filters = new Dictionary<string, string>
            {
                { "userId", request.UserId.ToString() },
                { "offset", offset.ToString() },
                { "limit", limit.ToString() }
            };
            _logger.LogQueryWithFilters("GetTasksByUser", filters, tasks.Count);

            return new PaginatedUserTasksResponse
            {
                UserId = request.UserId,
                UserDisplayName = user.DisplayName,
                Tasks = tasks,
                TotalCount = totalCount,
                Offset = offset,
                Limit = limit,
                HasMore = (offset + limit) < totalCount
            };
        }
        catch (Exception ex)
        {
            _logger.LogQueryError("GetTasksByUser", ex);
            throw;
        }
    }
}
