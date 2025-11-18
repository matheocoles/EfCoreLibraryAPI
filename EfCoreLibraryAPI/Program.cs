using EfCoreLibraryAPI;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthenticationJwtBearer(s => s.SigningKey = "Thesecretusedtosigntokens")
    .AddAuthentication();
    
builder.Services.AddAuthorization();   

builder.Services.AddFastEndpoints().SwaggerDocument(
    options => 
    {
        options.ShortSchemaNames = true;
    });

builder.Services.AddDbContext<LibraryDbContext>();

WebApplication app = builder.Build();
app.UseAuthorization()
    .UseAuthentication();
app.UseFastEndpoints(options =>
    {
        options.Endpoints.RoutePrefix = "API";
        options.Endpoints.ShortNames = true;
    }
    ).UseSwaggerGen();

app.UseHttpsRedirection();

app.Run();