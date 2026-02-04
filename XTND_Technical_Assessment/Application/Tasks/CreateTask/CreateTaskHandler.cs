using MediatR;
using Microsoft.EntityFrameworkCore;
using XTND_Technical_Assessment.Infra;    
using XTND_Technical_Assessment.Domain;  
using XTND_Technical_Assessment.Infrastructure.Logging;

namespace XTND_Technical_Assessment.Application.Tasks.CreateTask;

public sealed class CreateTaskHandler : IRequestHandler<CreateTaskCommand, int>
{
    private readonly Infra.AppDbContext _db;
    private readonly ILogger<CreateTaskHandler> _logger;

    public CreateTaskHandler(Infra.AppDbContext db, ILogger<CreateTaskHandler> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<int> Handle(CreateTaskCommand request, CancellationToken ct)
    {
        try
        {
            var userExists = await _db.TaskUsers.AnyAsync(u => u.Id == request.TaskUserId, ct);
            if (!userExists)
            {
                _logger.LogValidationError("Task", $"User with ID {request.TaskUserId} does not exist");
                throw new InvalidOperationException("Task user not found.");
            }

            var task = new TaskItem
            {
                Title = request.Title.Trim(),
                TaskUserId = request.TaskUserId,
                TaskStatusId = 1
            };

            _db.TaskItems.Add(task);
            await _db.SaveChangesAsync(ct);

            _logger.LogOperation("CREATE", "Task", task.Id);
            return task.Id;
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogOperationError("CREATE", "Task", ex);
            throw;
        }
    }
}
