using System.ComponentModel.DataAnnotations;

namespace EfCoreLibraryAPI.Models;

public class Author
{
    [Key] public int Id { get; set; }
    [Required, MaxLength(100)] public string? Name { get; set; }
    [Required, MaxLength(100)] public string? FirstName { get; set; }
    
    public List<Book>? Books { get; set; }
}