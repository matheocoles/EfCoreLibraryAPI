using EfCoreLibraryAPI.DTO.User.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.User;

public class GetUserRequest
{
    public int Id { get; set; }
}

public class GetUserEndpoint(LibraryDbContext libraryDbContext) : Endpoint<GetUserRequest, GetUserDto>
{
    public override void Configure()
    {
        Get("/api/users/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetUserRequest req, CancellationToken ct)
    {
        var user = await libraryDbContext.Users.FirstOrDefaultAsync(u => u.Id == req.Id);

        if (user == null)
        {
            await Send.NotFoundAsync(ct);
        }

        var getUserDto = new GetUserDto
        {
            Id = user.Id,
            Name = user.Name,
            FirstName = user.FirstName,
            Email = user.Email,
            Birthday = user.Birthday
        };
        
        await Send.OkAsync(getUserDto, ct);
    }
}