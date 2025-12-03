using ApiEfCoreLibrary.DTO.Login.Response;
using EfCoreLibraryAPI.DTO.Login.Request;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using PasswordGenerator;

namespace EfCoreLibraryAPI.Endpoints.Login;

public class UpdateLoginEndpoint(LibraryDbContext database) : Endpoint<UpdateLoginDto, GetLoginDto>
{
    public override void Configure()
    {
        Put("/logins/{@Id}", x => new {x.Id});
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateLoginDto req, CancellationToken ct)
    {
        Models.Login? login = await database.Logins.SingleOrDefaultAsync(x => x.Id == req.Id, ct);

        if (login == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        string? salt = new Password().IncludeLowercase().IncludeUppercase().IncludeNumeric().LengthRequired(24).Next();
        
        login.Username = req.Username;
        login.FullName = req.FullName;
        login.Password = BCrypt.Net.BCrypt.HashPassword(req.Password + salt);
        login.Salt = salt;
        await database.SaveChangesAsync(ct);
        
        GetLoginDto responseDto = new()
        {
            Id = login.Id,
            Username = login.Username,
            FullName = login.FullName,
            Password = login.Password,
            Salt = login.Salt
        };
        
        await Send.OkAsync(responseDto, ct);
    }
}