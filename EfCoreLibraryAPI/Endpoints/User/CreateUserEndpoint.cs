using EfCoreLibraryAPI.DTO.User.Request;
using EfCoreLibraryAPI.DTO.User.Response;
using FastEndpoints;

namespace EfCoreLibraryAPI.Endpoints.User;

public class CreateUserEndpoint(LibraryDbContext libraryDbContext) :Endpoint<CreateUserDto, GetUserDto>
{
    public override void Configure()
    {
        Post(("/users"));
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateUserDto req, CancellationToken ct)
    {
        Models.User user = new()
        {
            Name = req.Name,
            FirstName = req.Firstname,
            Email = req.Email,
            Birthday = req.Birthday
        };

        libraryDbContext.Users.Add(user);
        await libraryDbContext.SaveChangesAsync(ct);

        Console.WriteLine("Utilisateur créé avec succès !");

        GetUserDto responseDto = new()
        {
            Id = user.Id,
            Name = req.Name,
            Email = req.Email,
            Birthday = req.Birthday
        };

        await Send.OkAsync(responseDto, ct);
    }
}