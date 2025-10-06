using EfCoreLibraryAPI.DTO.Actor.Request;
using EfCoreLibraryAPI.DTO.Book.Request;
using EfCoreLibraryAPI.DTO.Book.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.Book;

public class CreateBookEndpoint(LibraryDbContext libraryDbContext) : Endpoint<CreateBookDto, GetBookDto, CreateAuthorDto>
{
    public override void Configure()
    {
        Post("/api/books");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateBookDto req, CancellationToken ct)
    {
        List<Models.Author> author = await libraryDbContext
            .Authors
            .Select(a => new Models.Author { Id = a.Id, Name = a.Name })
            .ToListAsync();
        
        if (author == null)
        {
            await Send.NotFoundAsync();
            return;
        }
        
        Models.Book book = new()
        {
            Title = req.Title,
            ReleaseYear = req.ReleaseYear,
            ISBN = req.ISBN,
            AuthorId = req.AuthorId
        };
        
        libraryDbContext.Books.Add(book);
        await libraryDbContext.SaveChangesAsync(ct);
        
        Console.WriteLine("LIvre créé avec succès");

        GetBookDto responseDto = new()
        {
            Id = book.Id,
            Title = req.Title,
            ReleaseYear = req.ReleaseYear,
            ISBN = req.ISBN,
            AuthorId = req.AuthorId
        };

        await Send.OkAsync(responseDto, ct);
    }
}