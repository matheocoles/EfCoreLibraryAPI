using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.User;

public class DeleteUserRequest
{
    public int Id { get; set; }
}

public class DeleteUserEndpoint(LibraryDbContext libraryDbContext) : Endpoint<DeleteUserRequest>
{
    public override void Configure()
    {
        Delete("/api/users/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteUserRequest req, CancellationToken ct)
    {
        var users = await libraryDbContext.Users.FirstOrDefaultAsync(a => a.Id == req.Id);

        if (users == null)
        {
            await Send.NotFoundAsync(ct);
        }
        
        libraryDbContext.Users.Remove(users);
        await libraryDbContext.SaveChangesAsync(ct);
        
        await Send.OkAsync(users, ct);
    }
}