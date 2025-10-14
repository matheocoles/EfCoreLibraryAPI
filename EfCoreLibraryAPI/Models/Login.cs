using System.ComponentModel.DataAnnotations;

namespace EfCoreLibraryAPI.Models;

public class Login
{
    [Key] public int Id {get; set;}
    [Required] public string? Username {get; set;}
    [Required] public string? Fullname {get; set;}
    [Required, Length(60, 60)] public string? Password {get; set;}
    [Required, Length(24,24)] public string? Salt { get; set; }
}