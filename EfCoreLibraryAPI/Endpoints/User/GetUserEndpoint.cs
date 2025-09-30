using EfCoreLibraryAPI.DTO.User.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.User;

public class GetUserRequest
{
    public int Id { get; set; }
}

public class GetUserEndpoint(LibraryDbContext libraryDbContext) : Endpoint<GetUserRequest, GetUserDto>
{
    public override void Configure()
    {
        Get("/api/users/{@id}", x => new { x.Id });
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetUserRequest req, CancellationToken ct)
    {
        Models.User? user = await libraryDbContext
            .Users
            .FirstOrDefaultAsync(u => u.Id == req.Id, cancellationToken: ct);

        if (user == null)
        {
            Console.WriteLine($"Aucun utilisateur avec l'ID {req.Id} trouvé.");
            await Send.NotFoundAsync(ct);
            return;
        }

        GetUserDto responseDto = new ()
        {
            Id = user.Id,
            Name = user.Name,
            FirstName = user.FirstName,
            Email = user.Email,
            Birthday = user.Birthday
        };
        
        await Send.OkAsync(responseDto, ct);
    }
}