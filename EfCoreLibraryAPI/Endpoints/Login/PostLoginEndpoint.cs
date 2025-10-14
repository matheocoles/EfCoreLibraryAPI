using EfCoreLibraryAPI.DTO.Login.Request;
using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.Login;

public class PostLoginEndpoint(LibraryDbContext libraryDbContext, IConfiguration config) : Endpoint<LoginRequest>
{ 
    public override void Configure()
    {
        Post("/api/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        var user = await libraryDbContext.Logins.SingleOrDefaultAsync(u => u.Username == req.Username, ct);

        if (user is null)
        {
            await Send.UnauthorizedAsync(ct);
            return;
        }

        var passwordWithSalt = req.Password + user.Salt;

        var valid = BCrypt.Net.BCrypt.Verify(passwordWithSalt, user.Password);

        if (!valid)
        {
            await Send.UnauthorizedAsync(ct);
            return;
        }

        var signingKey = config["Jwt:Key"] ?? "ChangeThisInProduction";
        var jwtToken = JwtBearer.CreateToken(o =>
        {
            o.SigningKey = signingKey;
            o.ExpireAt = DateTime.UtcNow.AddDays(1);
            o.User.Claims.Add(("UserName", req.Username));
            o.User["UserId"] = user.Id.ToString();
        });

        await Send.OkAsync(new
        {
            user.Username,
            user.Fullname,
            Token = jwtToken
        }, ct);
    }
}
