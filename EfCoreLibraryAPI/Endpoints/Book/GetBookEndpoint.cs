using EfCoreLibraryAPI.DTO.Actor.Response;
using EfCoreLibraryAPI.DTO.Book.Response;
using EfCoreLibraryAPI.Endpoints.Author;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.Book;

public class GetBookRequest
{
    public int Id { get; set; }
}

public class GetBookEndpoint(LibraryDbContext libraryDbContext) :Endpoint<GetBookRequest, GetBookDto, GetAuthorDto>
{
    public override void Configure()
    {
        Get("/books/{@id}", x => new { x.Id });
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetBookRequest req, CancellationToken ct)
    {
        Models.Book? book = await libraryDbContext
            .Books
            .Include(b => b.Author)
            .SingleOrDefaultAsync(b => b.Id == req.Id, cancellationToken: ct);

        if (book == null)
        {
            Console.WriteLine($"Aucun livre avec l'ID {req.Id} trouvé.");
            await Send.NotFoundAsync(ct);
            return;
        }

        GetBookDto responseDto = new()
        {
            Id = req.Id,
            Title = book.Title,
            ReleaseYear = book.ReleaseYear,
            Isbn = book.Isbn,
            AuthorId = book.AuthorId,
            AuthorName = book.Author?.Name,
            AuthorFirstName = book.Author?.FirstName,
        };

        await Send.OkAsync(responseDto, ct);
    }
}