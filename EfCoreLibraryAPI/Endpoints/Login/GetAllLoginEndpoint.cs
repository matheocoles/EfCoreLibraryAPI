using ApiEfCoreLibrary.DTO.Login.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.Login;

public class GetAllLoginEndpoint(LibraryDbContext database) : EndpointWithoutRequest<List<GetLoginDto>>
{
    public override void Configure()
    {
        Get("/logins");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        List<GetLoginDto> logins = await database.Logins
            .Select(login => new GetLoginDto()
            {
                Id = login.Id,
                Username = login.Username,
                FullName = login.FullName,
                Password = login.Password,
                Salt = login.Salt
            })
            .ToListAsync(ct);
        
        await Send.OkAsync(logins, ct);
    }
}