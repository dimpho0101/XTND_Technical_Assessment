using MediatR;
using XTND_Technical_Assessment.API.Contracts;

namespace XTND_Technical_Assessment.Application.Tasks.GetTaskById;

public sealed record GetTaskByIdQuery(int Id) : IRequest<GetTaskByIdResponse>;
