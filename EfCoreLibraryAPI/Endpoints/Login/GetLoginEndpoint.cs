using ApiEfCoreLibrary.DTO.Login.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.Login;

public class GetLoginRequest
{
    public int Id { get; set; }
}

public class GetLoginEndpoint(LibraryDbContext database) : Endpoint<GetLoginRequest, GetLoginDto>
{
    public override void Configure()
    {
        Get("/logins/{@Id}", x => new {x.Id});
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetLoginRequest req, CancellationToken ct)
    {
        EfCoreLibraryAPI.Models.Login? login = await database.Logins
            .SingleOrDefaultAsync(x => x.Id == req.Id, ct);

        if (login == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        
        GetLoginDto responseDto = new()
        {
            Id = login.Id,
            Username = login.Username,
            FullName = login.FullName,
            Password = login.Password,
            Salt = login.Salt
        };
        
        await Send.OkAsync(responseDto, ct);
    }
}