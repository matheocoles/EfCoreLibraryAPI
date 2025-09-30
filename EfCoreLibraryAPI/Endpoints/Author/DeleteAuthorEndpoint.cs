using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.Author;

public class DeleteAuthorRequest
{
    public int Id { get; set; }
}

public class DeleteAuthorEndpoint(LibraryDbContext libraryDbContext) : Endpoint<DeleteAuthorRequest>
{
    public override void Configure()
    {
        Delete("/api/authors/{id}");
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(DeleteAuthorRequest req, CancellationToken ct)
    {
        var author = await libraryDbContext.Authors.FirstOrDefaultAsync(a => a.Id == req.Id, ct);

        if (author == null)
        {
            await Send.NotFoundAsync(ct);
        }

        libraryDbContext.Authors.Remove(author);
        await libraryDbContext.SaveChangesAsync(ct);

        await Send.OkAsync(author, ct);
    }
}