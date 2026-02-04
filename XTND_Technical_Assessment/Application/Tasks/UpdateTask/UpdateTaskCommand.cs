using MediatR;

namespace XTND_Technical_Assessment.Application.Tasks.UpdateTask;

public sealed record UpdateTaskCommand(int Id, string Title, int TaskStatusId) : IRequest<bool>;
