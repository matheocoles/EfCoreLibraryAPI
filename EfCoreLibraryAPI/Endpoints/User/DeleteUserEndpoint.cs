using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.User;

public class DeleteUserRequest
{
    public int Id { get; set; }
}

public class DeleteUserEndpoint(LibraryDbContext libraryDbContext) :Endpoint<DeleteUserRequest>
{
    public override void Configure()
    {
        Delete("/api/users/{@id}", x => new { x.Id });
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteUserRequest req, CancellationToken ct)
    {
        Models.User? userToDelete = await libraryDbContext
            .Users
            .SingleOrDefaultAsync(a => a.Id == req.Id, cancellationToken: ct);

        if (userToDelete == null)
        {
            Console.WriteLine($"Aucun utilisateur avec l'ID {req.Id} trouvé.");
            await Send.NotFoundAsync(ct);
            return;
        }

        libraryDbContext.Users.Remove(userToDelete);
        await libraryDbContext.SaveChangesAsync(ct);

        await Send.NoContentAsync(ct);
    }
}