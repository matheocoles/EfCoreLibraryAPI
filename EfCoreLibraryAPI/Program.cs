using EfCoreLibraryAPI;
using FastEndpoints;
using FastEndpoints.Swagger;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// On ajoute ici FastEndpoints, un framework REPR et Swagger aux services disponibles dans le projet
builder.Services.AddFastEndpoints().SwaggerDocument();

// On ajoute ici la configuration de la base de données
builder.Services.AddDbContext<LibraryDbContext>();

// On construit l'application en lui donnant vie
WebApplication app = builder.Build();
app.UseFastEndpoints().UseSwaggerGen();

app.UseHttpsRedirection();

app.Run(); 