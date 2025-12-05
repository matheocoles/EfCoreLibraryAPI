using EfCoreLibraryAPI.DTO.User.Request;
using EfCoreLibraryAPI.DTO.User.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.User;

public class UpdateUserEndpoint(LibraryDbContext libraryDbContext) :Endpoint<UpdateUserDto, GetUserDto>
{
    public override void Configure()
    {
        Put("/users/{@id}", x => new { x.Id });
    }

    public override async Task HandleAsync(UpdateUserDto req, CancellationToken ct)
    {
        Models.User? userToEdit = await libraryDbContext
            .Users
            .SingleOrDefaultAsync(a => a.Id == req.Id, cancellationToken: ct);

        if (userToEdit == null)
        {
            Console.WriteLine($"Aucun author avec l'ID {req.Id} trouvé.");
            await Send.NotFoundAsync(ct);
            return;
        }

        userToEdit.Name = req.Name;
        userToEdit.FirstName = req.FirstName;
        userToEdit.Email = req.Email;
        userToEdit.Birthday = req.Birthday;

        await libraryDbContext.SaveChangesAsync(ct);

        GetUserDto responseDto = new()
        {
            Id = req.Id,
            Name = req.Name,
            FirstName = req.FirstName,
            Email = req.Email,
            Birthday = req.Birthday
        };

        await Send.OkAsync(responseDto, ct);
    }
}