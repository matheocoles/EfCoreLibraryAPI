using System.ComponentModel.DataAnnotations;

namespace EfCoreLibraryAPI.Models;

public class Login
{
    [Key] public int Id {get; set;}
    public string? Username {get; set;}
    public string? Fullname {get; set;}
    public string? Password {get; set;}
    public string? Salt { get; set; }
}