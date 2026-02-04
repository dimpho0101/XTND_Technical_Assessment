using MediatR;
using Microsoft.EntityFrameworkCore;
using XTND_Technical_Assessment.Infra;
using XTND_Technical_Assessment.Infrastructure.Logging;

namespace XTND_Technical_Assessment.Application.Tasks.UpdateTask;

public sealed class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, bool>
{
    private readonly AppDbContext _db;
    private readonly ILogger<UpdateTaskHandler> _logger;

    public UpdateTaskHandler(AppDbContext db, ILogger<UpdateTaskHandler> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<bool> Handle(UpdateTaskCommand request, CancellationToken ct)
    {
        try
        {
            var task = await _db.TaskItems.FirstOrDefaultAsync(t => t.Id == request.Id, ct);
            
            if (task is null)
            {
                _logger.LogNotFound("Task", request.Id);
                return false;
            }

            var statusExists = await _db.TaskStatuses.AnyAsync(s => s.Id == request.TaskStatusId, ct);
            if (!statusExists)
            {
                _logger.LogValidationError("Task", $"Status with ID {request.TaskStatusId} does not exist");
                return false;
            }

            task.Title = request.Title.Trim();
            task.TaskStatusId = request.TaskStatusId;

            _db.TaskItems.Update(task);
            await _db.SaveChangesAsync(ct);

            _logger.LogOperation("UPDATE", "Task", task.Id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogOperationError("UPDATE", "Task", ex);
            throw;
        }
    }
}
