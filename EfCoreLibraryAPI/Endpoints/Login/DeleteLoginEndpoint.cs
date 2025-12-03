using ApiEfCoreLibrary.DTO.Login.Response;
using EfCoreLibraryAPI;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiEfCoreLibrary.Endpoints.Login;

public class DeleteLoginRequest
{
    public int Id { get; set; }
}

public class DeleteLoginEndpoint(LibraryDbContext database) : Endpoint<DeleteLoginRequest>
{
    public override void Configure()
    {
        Delete("/logins/{@Id}", x => new {x.Id});
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteLoginRequest req, CancellationToken ct)
    {
        EfCoreLibraryAPI.Models.Login? login = await database.Logins.SingleOrDefaultAsync(x => x.Id == req.Id, ct);

        if (login == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        
        database.Logins.Remove(login);
        await database.SaveChangesAsync(ct);
        
        await Send.NoContentAsync(ct);
    }
}