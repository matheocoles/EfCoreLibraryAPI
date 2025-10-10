using System.ComponentModel.DataAnnotations;

namespace EfCoreLibraryAPI.Models;

public class User
{
    [Key] public int Id { get; set; }
    [Required, MaxLength(100)] public string? Name { get; set; }
    [Required, MaxLength(100)] public string? FirstName { get; set; }
    [Required, MaxLength(255)] public string? Email { get; set; }
    public DateOnly Birthday { get; set; }

    public List<Loan>? Loans { get; set; }
}