using EfCoreLibraryAPI.DTO.Actor.Request;
using EfCoreLibraryAPI.DTO.Actor.Response;
using FastEndpoints;
using Microsoft.AspNetCore.Server.Kestrel;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.Author;

public class UpdateAuthorEndpoint(LibraryDbContext libraryDbContext) : Endpoint<UpdateAuthorDto, GetAuthorDto> 
{
    public override void Configure()
    {
        Put("/api/authors/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateAuthorDto req, CancellationToken ct)
    {
        var author = await libraryDbContext.Authors.FirstOrDefaultAsync(a =>  a.Id == req.Id);

        if (author == null)
        {
            await Send.NotFoundAsync(ct);
        }
        
        author.Name = req.Name;
        author.FirstName = req.FristName;
        
        await libraryDbContext.SaveChangesAsync(ct);
        
        var getAuthorDto = new GetAuthorDto()
        {
            Id = author.Id,
            Name = author.Name,
            FirstName = author.FirstName
        };

        await Send.OkAsync(getAuthorDto, ct);
    }

}