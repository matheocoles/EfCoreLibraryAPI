using EfCoreLibraryAPI.DTO.User.Request;
using EfCoreLibraryAPI.DTO.User.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.User;

public class UpdateUserEndpoint(LibraryDbContext libraryDbContext) : Endpoint<UpdateUserDto, GetUserDto>
{
    public override void Configure()
    {
        Put("/api/users/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateUserDto req, CancellationToken ct)
    {
        var user = await libraryDbContext.Users.FirstOrDefaultAsync(a => a.Id == req.Id);

        if (user == null)
        {
            await Send.NotFoundAsync(ct);
        }
        
        user.Name = req.Name;
        user.FirstName = req.FirstName;
        user.Email = req.Email;
        user.Birthday = req.Birthday;
        
        await libraryDbContext.SaveChangesAsync(ct);

        var getUserDto = new GetUserDto()
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