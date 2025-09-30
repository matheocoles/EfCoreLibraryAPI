using EfCoreLibraryAPI.DTO.Book.Request;
using EfCoreLibraryAPI.DTO.Book.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.Book;

public class UpdateBookEndpoint(LibraryDbContext libraryDbContext) :Endpoint<UpdateBookDto, GetBookDto>
{
    public override void Configure()
    {
        Put("/api/book/{@id}", x => new { x.Id });
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateBookDto req, CancellationToken ct)
    {
        Models.Book? bookToEdit = await libraryDbContext
            .Books
            .SingleOrDefaultAsync(b => b.Id == req.Id, cancellationToken: ct);

        if (bookToEdit == null)
        {
            Console.WriteLine($"Aucun livre avec l'ID {req.Id} trouvé.");
            await Send.NotFoundAsync(ct);
            return;
        }

        bookToEdit.Title = req.Title;
        bookToEdit.ReleaseYear = req.ReleaseYear;
        bookToEdit.ISBN = req.ISBN;

        await libraryDbContext.SaveChangesAsync(ct);

        GetBookDto responseDto = new()
        {
            Id = req.Id,
            Title = req.Title,
            ReleaseYear = req.ReleaseYear,
            ISBN = req.ISBN
        };

        await Send.OkAsync(responseDto, ct);
    }
}

