using EfCoreLibraryAPI.DTO.User.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.User;

public class GetAllUsersEndpoint(LibraryDbContext libraryDbContext) :EndpointWithoutRequest<List<GetUserDto>>
{
    public override void Configure()
    {
        Get("/api/users");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        List<GetUserDto> responseDto = await libraryDbContext
            .Users
            .Select(u => new GetUserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    FirstName = u.FirstName,
                    Email = u.Email,
                    Birthday = u.Birthday
                }
            ).ToListAsync(ct);

        await Send.OkAsync(responseDto, ct);
    }
}