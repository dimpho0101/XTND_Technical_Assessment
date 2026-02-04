using MediatR;

namespace XTND_Technical_Assessment.Application.Tasks.CreateTask;

public sealed record CreateTaskCommand(string Title, int TaskUserId) : IRequest<int>;

