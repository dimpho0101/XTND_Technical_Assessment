using MediatR;

namespace XTND_Technical_Assessment.Application.Users.DeleteUser;

public sealed record DeleteUserCommand(int Id) : IRequest<bool>;
