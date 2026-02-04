using MediatR;
using XTND_Technical_Assessment.API.Contracts;

namespace XTND_Technical_Assessment.Application.Tasks.GetTasksByUser;

public sealed record GetTasksByUserQuery(int UserId, int Offset = 0, int Limit = 10) : IRequest<PaginatedUserTasksResponse>;
