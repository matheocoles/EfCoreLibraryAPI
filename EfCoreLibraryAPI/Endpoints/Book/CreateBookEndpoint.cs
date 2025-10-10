using EfCoreLibraryAPI.DTO.Book.Request;
using EfCoreLibraryAPI.DTO.Book.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.Book;

public class CreateBookEndpoint(LibraryDbContext libraryDbContext) :Endpoint<CreateBookDto, GetBookDto>
{
    public override void Configure()
    {
        Post("/api/books");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateBookDto req, CancellationToken ct)
    {
        var author = await libraryDbContext.Authors
            .FirstOrDefaultAsync(a => a.Id == req.AuthorId, ct);

        if (author == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        Models.Book book = new()
        {
            Title = req.Title,
            ReleaseYear = req.ReleaseYear,
            Isbn = req.Isbn,
            AuthorId = req.AuthorId
        };


        libraryDbContext.Books.Add(book);
        await libraryDbContext.SaveChangesAsync(ct);

        Console.WriteLine("Livre créé avec succès");

        GetBookDto responseDto = new()
        {
            Id = book.Id,
            Title = req.Title,
            ReleaseYear = req.ReleaseYear,
            Isbn = req.Isbn,
            AuthorId = req.AuthorId,
            AuthorName = req.AuthorName,
            AuthorFirstName = req.AuthorFirstName
        };

        await Send.OkAsync(responseDto, ct);
    }
}