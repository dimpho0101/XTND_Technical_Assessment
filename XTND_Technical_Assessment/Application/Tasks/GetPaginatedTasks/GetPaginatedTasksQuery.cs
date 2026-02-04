using MediatR;
using XTND_Technical_Assessment.API.Contracts;

namespace XTND_Technical_Assessment.Application.Tasks.GetPaginatedTasks;

public sealed record GetPaginatedTasksQuery(int Offset, int Limit) : IRequest<PaginatedTasksResponse>;
