using MediatR;

namespace XTND_Technical_Assessment.Application.Tasks.DeleteTask;

public sealed record DeleteTaskCommand(int Id) : IRequest<bool>;
