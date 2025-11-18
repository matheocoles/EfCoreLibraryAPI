using EfCoreLibraryAPI.DTO.Actor.Response;
using EfCoreLibraryAPI.DTO.Book.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.Book;

public class GetAllBooksEndpoint(LibraryDbContext libraryDbContext) :EndpointWithoutRequest<List<GetBookDto>>
{
    public override void Configure()
    {
        Get("/books");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        List<GetBookDto> responseDto = await libraryDbContext.Books
            .Include(b => b.Author)
            .Select(b => new GetBookDto()
                {
                    Id = b.Id,
                    Title = b.Title,
                    ReleaseYear = b.ReleaseYear,
                    Isbn = b.Isbn,
                    AuthorId = b.AuthorId,
                    AuthorName = b.Author!.Name,
                    AuthorFirstName = b.Author!.FirstName,
                }
            ).ToListAsync(ct);

        await Send.OkAsync(responseDto, ct);
    }
}