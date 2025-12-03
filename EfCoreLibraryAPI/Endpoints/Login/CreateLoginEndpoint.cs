using ApiEfCoreLibrary.DTO.Login.Response;
using EfCoreLibraryAPI.DTO.Login.Request;
using FastEndpoints;
using PasswordGenerator;

namespace EfCoreLibraryAPI.Endpoints.Login;

public class CreateLoginEndpoint(LibraryDbContext database) : Endpoint<CreateLoginDto, GetLoginDto>
{
    public override void Configure()
    {
        Post("/logins");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateLoginDto req, CancellationToken ct)
    {
        string? salt = new Password().IncludeLowercase().IncludeUppercase().IncludeNumeric().LengthRequired(24).Next();
        
        Models.Login login = new Models.Login()
        {
            Username = req.Username,
            FullName = req.FullName,
            Password = BCrypt.Net.BCrypt.HashPassword(req.Password + salt),
            Salt = salt
        };
        
        database.Logins.Add(login);
        
        await database.SaveChangesAsync(ct);
        // Pour renvoyer une erreur : Send.StringAsync("Le message d'erreur", 400);
        
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