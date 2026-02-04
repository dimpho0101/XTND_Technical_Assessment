using MediatR;

namespace XTND_Technical_Assessment.Application.Users.CreateUser;

public sealed record CreateUserCommand(string DisplayName) : IRequest<int>;
