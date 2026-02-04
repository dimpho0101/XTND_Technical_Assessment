using MediatR;
using XTND_Technical_Assessment.Infra;
using XTND_Technical_Assessment.Domain;
using XTND_Technical_Assessment.Infrastructure.Logging;

namespace XTND_Technical_Assessment.Application.Users.CreateUser;

public sealed class CreateUserHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly AppDbContext _db;
    private readonly ILogger<CreateUserHandler> _logger;

    public CreateUserHandler(AppDbContext db, ILogger<CreateUserHandler> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken ct)
    {
        try
        {
            var user = new TaskUser
            {
                DisplayName = request.DisplayName.Trim()
            };

            _db.TaskUsers.Add(user);
            await _db.SaveChangesAsync(ct);

            _logger.LogOperation("CREATE", "User", user.Id);
            return user.Id;
        }
        catch (Exception ex)
        {
            _logger.LogOperationError("CREATE", "User", ex);
            throw;
        }
    }
}
