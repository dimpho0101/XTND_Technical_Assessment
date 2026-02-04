using MediatR;
using Microsoft.EntityFrameworkCore;
using XTND_Technical_Assessment.Infra;

namespace XTND_Technical_Assessment.API.Contracts;

public sealed class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdResponse?>
{
    private readonly Infra.AppDbContext _db;

    public GetUserByIdHandler(Infra.AppDbContext db) => _db = db;

    public async Task<GetUserByIdResponse?> Handle(GetUserByIdQuery request, CancellationToken ct)
    {
        return await _db.TaskUsers
            .Where(u => u.Id == request.Id)
            .Select(u => new GetUserByIdResponse
            {
                Id = u.Id,
                DisplayName = u.DisplayName
            })
            .FirstOrDefaultAsync(ct);
    }
}
