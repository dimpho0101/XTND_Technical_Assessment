using MediatR;
using Microsoft.EntityFrameworkCore;
using XTND_Technical_Assessment.Infra;
using XTND_Technical_Assessment.Infrastructure.Logging;

namespace XTND_Technical_Assessment.Application.Users.DeleteUser;

public sealed class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly AppDbContext _db;
    private readonly ILogger<DeleteUserHandler> _logger;

    public DeleteUserHandler(AppDbContext db, ILogger<DeleteUserHandler> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken ct)
    {
        try
        {
            var user = await _db.TaskUsers.FirstOrDefaultAsync(u => u.Id == request.Id, ct);
            
            if (user is null)
            {
                _logger.LogNotFound("User", request.Id);
                return false;
            }

            var hasTasksCount = await _db.TaskItems.CountAsync(t => t.TaskUserId == request.Id, ct);
            if (hasTasksCount > 0)
            {
                _logger.LogConstraintViolation("User", 
                    $"Cannot delete user with {hasTasksCount} assigned task(s)");
                throw new InvalidOperationException("Cannot delete user with existing tasks.");
            }

            _db.TaskUsers.Remove(user);
            await _db.SaveChangesAsync(ct);

            _logger.LogOperation("DELETE", "User", request.Id);
            return true;
        }
        catch (InvalidOperationException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogOperationError("DELETE", "User", ex);
            throw;
        }
    }
}
