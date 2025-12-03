using System.ComponentModel.DataAnnotations;

namespace EfCoreLibraryAPI.Models;

public class Login
{
    [Key] public int Id { get; set; }
    [Required, MaxLength(100)] public string? Username { get; set; }
    [Required, MaxLength(200)] public string? FullName { get; set; }
    [Required, Length(60, 60)] public string? Password { get; set; }
    [Required, Length(24, 24)] public string? Salt { get; set; }
    [Required, MaxLength(100)] public string? Role { get; set; }
}