using MediatR;

namespace XTND_Technical_Assessment.API.Contracts;

public sealed record GetUserByIdQuery(int Id) : IRequest<GetUserByIdResponse?>;
