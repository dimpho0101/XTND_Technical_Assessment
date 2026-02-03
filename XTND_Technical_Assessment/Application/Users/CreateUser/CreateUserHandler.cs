using MediatR;
using XTND_Technical_Assessment.Infra;
using XTND_Technical_Assessment.Domain;

namespace XTND_Technical_Assessment.Application.Users.CreateUser;

public sealed class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly AppDbContext _db;

    public CreateUserHandler(AppDbContext db) => _db = db;

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken ct)
    {
        var user = new TaskUser
        {
            Id = Guid.NewGuid(),
            DisplayName = request.DisplayName.Trim()
        };

        _db.TaskUsers.Add(user);
        await _db.SaveChangesAsync(ct);

        return user.Id;
    }
}
