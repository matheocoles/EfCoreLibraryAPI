using EfCoreLibraryAPI.DTO.Actor.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.Author;

public class GetAuthorRequest
{
    public int Id { get; set; }
}

public class GetAuthorEndpoint(LibraryDbContext libraryDbContext) : Endpoint<GetAuthorRequest, GetAuthorDto>
{
    public override void Configure()
    {
        Get("/api/authors/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetAuthorRequest req, CancellationToken ct)
    {
        var author = await libraryDbContext.Authors.FirstOrDefaultAsync(a => a.Id == req.Id);

        if (author == null)
        {
            await Send.NotFoundAsync(ct);
        }
        
        var getAuthorDto = new GetAuthorDto
        {
            Id = author.Id,
            Name = author.Name,
            FirstName = author.FirstName
        };

        await Send.OkAsync(getAuthorDto, ct);
    }
}