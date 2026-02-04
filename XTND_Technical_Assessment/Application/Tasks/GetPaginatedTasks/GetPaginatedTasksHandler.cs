using MediatR;
using Microsoft.EntityFrameworkCore;
using XTND_Technical_Assessment.Infra;
using XTND_Technical_Assessment.API.Contracts;
using XTND_Technical_Assessment.Infrastructure.Logging;

namespace XTND_Technical_Assessment.Application.Tasks.GetPaginatedTasks;

public sealed class GetPaginatedTasksHandler : IRequestHandler<GetPaginatedTasksQuery, PaginatedTasksResponse>
{
    private readonly AppDbContext _db;
    private readonly ILogger<GetPaginatedTasksHandler> _logger;

    public GetPaginatedTasksHandler(AppDbContext db, ILogger<GetPaginatedTasksHandler> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<PaginatedTasksResponse> Handle(GetPaginatedTasksQuery request, CancellationToken ct)
    {
        try
        {

            var offset = Math.Max(0, request.Offset);
            var limit = Math.Clamp(request.Limit, 1, 100); 


            var totalCount = await _db.TaskItems.CountAsync(ct);

            var tasks = await _db.TaskItems
                .Include(t => t.TaskUser)
                .Include(t => t.TaskStatus)
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
                { "offset", offset.ToString() },
                { "limit", limit.ToString() }
            };
            _logger.LogQueryWithFilters("GetPaginatedTasks", filters, tasks.Count);

            return new PaginatedTasksResponse
            {
                Tasks = tasks,
                TotalCount = totalCount,
                Offset = offset,
                Limit = limit,
                HasMore = (offset + limit) < totalCount
            };
        }
        catch (Exception ex)
        {
            _logger.LogQueryError("GetPaginatedTasks", ex);
            throw;
        }
    }
}
