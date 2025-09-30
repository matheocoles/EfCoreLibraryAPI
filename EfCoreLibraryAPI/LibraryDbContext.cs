using EfCoreLibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI;

public class LibraryDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<User> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=romaric-thibault.fr;" +
            "Database=matheo_EfCoreLibrary;" +
            "User Id=matheo;" +
            "Password=Onto9-Cage-Afflicted;" +
            "TrustServerCertificate=true;"
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder){}
}