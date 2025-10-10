using System.ComponentModel.DataAnnotations;
using EfCoreLibraryAPI.Models;

namespace EfCoreLibraryAPI.Models;

public class Book
{
    [Key] public int Id { get; set; }
    [Required, MaxLength(255)] public string? Title { get; set; }
    public int ReleaseYear { get; set; }
    [Required, MaxLength(20)] public string? Isbn { get; set; }
    
    [Required] public int AuthorId { get; set; }
    public Author? Author { get; set; }
    public List<Loan>? Loans { get; set; }
}