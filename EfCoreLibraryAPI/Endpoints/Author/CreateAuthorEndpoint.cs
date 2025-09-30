using EfCoreLibraryAPI.DTO.Actor.Request;
using EfCoreLibraryAPI.DTO.Actor.Response;
using FastEndpoints;

namespace EfCoreLibraryAPI.Endpoints.Author;

public class CreateAuthorEndpoint(LibraryDbContext libraryDbContext) : Endpoint<CreateAuthorDto, GetAuthorDto>
{
    public override void Configure()
    {
        Post("/api/authors");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateAuthorDto req, CancellationToken ct)
    {
        var author = new Models.Author
        {
            Name = req.Name,
            FirstName = req.FirstName
        };
        
        libraryDbContext.Authors.Add(author);
        await libraryDbContext.SaveChangesAsync(ct);

        var getAuthorDto = new GetAuthorDto()
        {
            Id = author.Id,
            Name = req.Name,
            FirstName = req.FirstName
        };

        await Send.OkAsync(getAuthorDto, ct);
    }
}