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

public class GetBookEndpoint(LibraryDbContext libraryDbContext) : Endpoint<GetBookRequest, GetBookDto, GetAuthorDto>
{
    public override void Configure()
    {
        Get("/api/books/{@id}", x => new { x.Id });
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

        // List<Models.Author> author = await libraryDbContext
        //     .Authors
        //     .Select(a => new Models.Author { Id = a.Id, Name = a.Name })
        //     .ToListAsync();
        //
        // if (author == null)
        // {
        //     await Send.NotFoundAsync();
        //     return;
        // }
        
        GetBookDto responseDto = new()
        {
            Id = req.Id, 
            Title = book.Title, 
            ReleaseYear = book.ReleaseYear,
            ISBN = book.ISBN,
            AuthorId = book.AuthorId,
            AuthorName = book.Author.Name,
            AuthorFirstName = book.Author.FirstName,
        };

        await Send.OkAsync(responseDto, ct);
    }
}