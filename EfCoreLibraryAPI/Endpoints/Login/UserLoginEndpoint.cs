using ApiEfCoreLibrary.DTO.Login.Response;
using EfCoreLibraryAPI.DTO.Login.Request;
using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.Login;

public class UserLoginEndpoint(LibraryDbContext database) : Endpoint<ConnectLoginDto, GetLoginConnectDto>
{
    public override void Configure()
    {
        Post("/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ConnectLoginDto req, CancellationToken ct)
    {
        Models.Login? login = await database.Logins.SingleOrDefaultAsync(x => x.Username == req.Username, ct);

        if (login == null)
        {
            await Send.UnauthorizedAsync(ct);
            return;
        }
        
        if (BCrypt.Net.BCrypt.Verify(req.Password + login.Salt, login.Password))
        {
            string jwtToken = JwtBearer.CreateToken(
                o =>
                {
                    o.SigningKey = "ThisIsASuperSecretJwtKeyThatIsAtLeast32CharsLong";
                    o.ExpireAt = DateTime.UtcNow.AddMinutes(15);
                    if (login.Role != null) o.User.Roles.Add(login.Role);
                    o.User.Claims.Add(("Username", login.Username)!);
                    o.User.Claims.Add(("FullName", login.FullName)!);
                    o.User["UserId"] = "001";
                });

            GetLoginConnectDto responseDto = new()
            {
                Token = jwtToken
            };
            
            await Send.OkAsync(responseDto, ct);
        }
        else await Send.UnauthorizedAsync(ct);
    }
}