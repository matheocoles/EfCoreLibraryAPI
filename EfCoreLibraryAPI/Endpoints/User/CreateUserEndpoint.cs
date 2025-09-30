using EfCoreLibraryAPI.DTO.User.Request;
using EfCoreLibraryAPI.DTO.User.Response;
using FastEndpoints;

namespace EfCoreLibraryAPI.Endpoints.User;

public class CreateUserEndpoint(LibraryDbContext libraryDbContext) : Endpoint<CreateUserDto, GetUserDto>
{
    public override void Configure()
    {
        Post(("/api/users"));
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateUserDto req, CancellationToken ct)
    {
        var user = new Models.User
        {
            Name = req.Name,
            FirstName = req.Firstname,
            Email = req.Email,
            Birthday = req.Birthday
        };
        
        libraryDbContext.Users.Add(user);
        await libraryDbContext.SaveChangesAsync(ct);

        var getUserDto = new GetUserDto()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Birthday = user.Birthday
        };

        await Send.OkAsync(getUserDto, ct);
    }
}