using EfCoreLibraryAPI;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// --- CONFIGURATION DES SERVICES ---

// 1. JWT (La clé correspond bien à celle du Endpoint maintenant)
builder.Services
    .AddAuthenticationJwtBearer(s => s.SigningKey = "ThisIsASuperSecretJwtKeyThatIsAtLeast32CharsLong")
    .AddAuthentication();

builder.Services.AddAuthorization();

// 2. CORS (Autorise Angular sur le port 4200)
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policyBuilder =>
            policyBuilder.WithOrigins("http://localhost:4200")
                .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE")
                .AllowAnyHeader()
                .AllowCredentials() // Ajout conseillé si tu envoies des tokens
    )
);

builder.Services.AddFastEndpoints().SwaggerDocument(options => 
{
    options.ShortSchemaNames = true;
});

builder.Services.AddDbContext<LibraryDbContext>();

WebApplication app = builder.Build();

// --- PIPELINE MIDDLEWARE (L'ORDRE EST CRITIQUE ICI) ---

// 1. HTTPS Redirection en premier
app.UseHttpsRedirection();

// 2. CORS juste après (pour accepter les requêtes d'Angular)
app.UseCors();

// 3. Authentification (Qui est l'utilisateur ?)
app.UseAuthentication();

// 4. Autorisation (A-t-il les droits ?)
app.UseAuthorization();

// 5. FastEndpoints (Exécution de la logique métier)
app.UseFastEndpoints(options =>
{
    options.Endpoints.RoutePrefix = "API"; // Ton login sera sur : /API/login
    options.Endpoints.ShortNames = true;
}).UseSwaggerGen();

app.Run();