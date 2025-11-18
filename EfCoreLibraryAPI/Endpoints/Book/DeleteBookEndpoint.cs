using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.Book;

public class DeleteBookRequest
{
    public int Id { get; set; }
}

public class DeleteBookEndpoint(LibraryDbContext libraryDbContext) :Endpoint<DeleteBookRequest>
{
    public override void Configure()
    {
        Delete("/books/{@id}", x => new { x.Id });
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteBookRequest req, CancellationToken ct)
    {
        Models.Book? bookToDelete = await libraryDbContext
            .Books
            .SingleOrDefaultAsync(b => b.Id == req.Id, cancellationToken: ct);

        if (bookToDelete == null)
        {
            Console.WriteLine($"Aucun livre avec l'ID {req.Id} trouvé.");
            await Send.NotFoundAsync(ct);
            return;
        }

        libraryDbContext.Books.Remove(bookToDelete);
        await libraryDbContext.SaveChangesAsync(ct);

        await Send.NoContentAsync(ct);
    }
}