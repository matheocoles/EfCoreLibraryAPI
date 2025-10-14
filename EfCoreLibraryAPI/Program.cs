using System.Net;
using EfCoreLibraryAPI;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthenticationJwtBearer(s => s.SigningKey = "The secret used to sign tokens")
    .AddAuthentication();
    
builder.Services.AddAuthorization();   

builder.Services.AddFastEndpoints().SwaggerDocument();

builder.Services.AddDbContext<LibraryDbContext>();

WebApplication app = builder.Build();
app.UseAuthorization()
    .UseAuthentication();
app.UseFastEndpoints().UseSwaggerGen();

app.UseHttpsRedirection();

app.Run();