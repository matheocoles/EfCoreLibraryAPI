using EfCoreLibraryAPI.DTO.Actor.Request;
using EfCoreLibraryAPI.DTO.Actor.Response;
using FastEndpoints;

namespace EfCoreLibraryAPI.Endpoints.Author;

public class CreateAuthorEndpoint(LibraryDbContext libraryDbContext) :Endpoint<CreateAuthorDto, GetAuthorDto>
{
    public override void Configure()
    {
        Post("/authors");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateAuthorDto req, CancellationToken ct)
    {
        Models.Author author = new()
        {
            Name = req.Name,
            FirstName = req.FirstName
        };

        libraryDbContext.Authors.Add(author);
        await libraryDbContext.SaveChangesAsync(ct);

        Console.WriteLine("Auteur créé avec succès !");

        GetAuthorDto responseDto = new()
        {
            Id = author.Id,
            Name = req.Name,
            FirstName = req.FirstName
        };

        await Send.OkAsync(responseDto, ct);
    }
}