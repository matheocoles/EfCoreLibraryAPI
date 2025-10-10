using EfCoreLibraryAPI.DTO.Actor.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.Author;

public class GetAllAuthorsEndpoint(LibraryDbContext libraryDbContext) :EndpointWithoutRequest<List<GetAuthorDto>>
{
    public override void Configure()
    {
        Get("/api/authors");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        List<GetAuthorDto> responseDto = await libraryDbContext.Authors
            .Select(a => new GetAuthorDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    FirstName = a.FirstName
                }
            ).ToListAsync(ct);

        await Send.OkAsync(responseDto, ct);
    }
}