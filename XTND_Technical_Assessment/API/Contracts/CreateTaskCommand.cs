using MediatR;

namespace XTND_Technical_Assessment.Application.Tasks.CreateTask;

public sealed record CreateTaskCommand(string Title, Guid TaskUserId) : IRequest<Guid>;
