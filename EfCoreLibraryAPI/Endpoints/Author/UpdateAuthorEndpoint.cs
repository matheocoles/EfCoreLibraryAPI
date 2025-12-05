using EfCoreLibraryAPI.DTO.Actor.Request;
using EfCoreLibraryAPI.DTO.Actor.Response;
using FastEndpoints;
using Microsoft.AspNetCore.Server.Kestrel;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.Author;

public class UpdateAuthorEndpoint(LibraryDbContext libraryDbContext) :Endpoint<UpdateAuthorDto, GetAuthorDto>
{
    public override void Configure()
    {
        Put("/authors/{@id}", x => new { x.Id });
    }

    public override async Task HandleAsync(UpdateAuthorDto req, CancellationToken ct)
    {
        Models.Author? authorToEdit = await libraryDbContext
            .Authors
            .SingleOrDefaultAsync(a => a.Id == req.Id, cancellationToken: ct);

        if (authorToEdit == null)
        {
            Console.WriteLine($"Aucun author avec l'ID {req.Id} trouvé.");
            await Send.NotFoundAsync(ct);
            return;
        }

        authorToEdit.Name = req.Name;
        authorToEdit.FirstName = req.FirstName;

        await libraryDbContext.SaveChangesAsync(ct);

        GetAuthorDto responseDto = new()
        {
            Id = req.Id,
            Name = req.Name,
            FirstName = req.FirstName,
        };

        await Send.OkAsync(responseDto, ct);
    }

}