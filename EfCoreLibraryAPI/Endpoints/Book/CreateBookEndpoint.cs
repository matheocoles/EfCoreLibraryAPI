using EfCoreLibraryAPI.DTO.Book.Request;
using EfCoreLibraryAPI.DTO.Book.Response;
using EfCoreLibraryAPI.DTO.User.Response;
using FastEndpoints;

namespace EfCoreLibraryAPI.Endpoints.Book;

public class CreateBookEndpoint(LibraryDbContext libraryDbContext) : Endpoint<CreateBookDto, GetBookDto>
{
    public override void Configure()
    {
        Post("/api/books");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateBookDto req, CancellationToken ct)
    {
        Models.Book book = new()
        {
            Title = req.Title,
            ReleaseYear = req.ReleaseYear,
            ISBN = req.ISBN
        };
        
        libraryDbContext.Books.Add(book);
        await libraryDbContext.SaveChangesAsync(ct);
        
        Console.WriteLine("LIvre créé avec succès");

        GetBookDto responseDto = new()
        {
            Id = book.Id,
            Title = req.Title,
            ReleaseYear = req.ReleaseYear,
            ISBN = req.ISBN
        };

        await Send.OkAsync(responseDto, ct);
    }
}