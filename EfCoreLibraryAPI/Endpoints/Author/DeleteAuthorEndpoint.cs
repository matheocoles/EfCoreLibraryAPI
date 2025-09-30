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
        Delete("/api/authors/{@id}", x => new { x.Id });
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(DeleteAuthorRequest req, CancellationToken ct)
    {
        Models.Author? authorToDelete = await libraryDbContext
            .Authors
            .SingleOrDefaultAsync(a => a.Id == req.Id, cancellationToken: ct);

        if (authorToDelete == null)
        {
            Console.WriteLine($"Aucun author avec l'ID {req.Id} trouvé.");
            await Send.NotFoundAsync(ct);
            return;
        }

        libraryDbContext.Authors.Remove(authorToDelete);
        await libraryDbContext.SaveChangesAsync(ct);

        await Send.NoContentAsync(ct);
    }
}