using MediatR;
using Microsoft.EntityFrameworkCore;
using XTND_Technical_Assessment.Infra;     // AppDbContext namespace
using XTND_Technical_Assessment.Domain;   // TaskItem/TaskUser namespace (adjust if needed)

namespace XTND_Technical_Assessment.Application.Tasks.CreateTask;

public sealed class CreateTaskHandler : IRequestHandler<CreateTaskCommand, Guid>
{
    private readonly AppDbContext _db;

    public CreateTaskHandler(AppDbContext db) => _db = db;

    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken ct)
    {
        // optional: make sure user exists
        var userExists = await _db.TaskUsers.AnyAsync(u => u.Id == request.TaskUserId, ct);
        if (!userExists)
            throw new InvalidOperationException("Task user not found.");

        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = request.Title.Trim(),
            TaskUserId = request.TaskUserId
            // created/updated timestamps should be set in SaveChanges override (what we did earlier)
        };

        _db.TaskItems.Add(task);
        await _db.SaveChangesAsync(ct);

        return task.Id;
    }
}
