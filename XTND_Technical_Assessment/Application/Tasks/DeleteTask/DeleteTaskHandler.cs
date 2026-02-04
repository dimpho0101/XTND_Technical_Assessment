using MediatR;
using Microsoft.EntityFrameworkCore;
using XTND_Technical_Assessment.Infra;
using XTND_Technical_Assessment.Infrastructure.Logging;

namespace XTND_Technical_Assessment.Application.Tasks.DeleteTask;

public sealed class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, bool>
{
    private readonly AppDbContext _db;
    private readonly ILogger<DeleteTaskHandler> _logger;

    public DeleteTaskHandler(AppDbContext db, ILogger<DeleteTaskHandler> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken ct)
    {
        try
        {
            var task = await _db.TaskItems.FirstOrDefaultAsync(t => t.Id == request.Id, ct);
            
            if (task is null)
            {
                _logger.LogNotFound("Task", request.Id);
                return false;
            }

            _db.TaskItems.Remove(task);
            await _db.SaveChangesAsync(ct);

            _logger.LogOperation("DELETE", "Task", request.Id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogOperationError("DELETE", "Task", ex);
            throw;
        }
    }
}
