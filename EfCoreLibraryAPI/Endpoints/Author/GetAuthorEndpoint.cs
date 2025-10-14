using EfCoreLibraryAPI.DTO.Actor.Response;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.Author;

public class GetAuthorRequest
{
    public int Id { get; set; }
}

public class GetAuthorEndpoint(LibraryDbContext libraryDbContext) :Endpoint<GetAuthorRequest, GetAuthorDto>
{
    public override void Configure()
    {
        Get("/api/authors/{@id}", x => new { x.Id });
    }

    public override async Task HandleAsync(GetAuthorRequest req, CancellationToken ct)
    {
        Models.Author? author = await libraryDbContext
            .Authors
            .SingleOrDefaultAsync(a => a.Id == req.Id, cancellationToken: ct);

        if (author == null)
        {
            Console.WriteLine($"Aucun author avec l'ID {req.Id} trouvé.");
            await Send.NotFoundAsync(ct);
            return;
        }

        GetAuthorDto responseDto = new()
        {
            Id = req.Id,
            Name = author.Name,
            FirstName = author.FirstName
        };

        await Send.OkAsync(responseDto, ct);
    }
}