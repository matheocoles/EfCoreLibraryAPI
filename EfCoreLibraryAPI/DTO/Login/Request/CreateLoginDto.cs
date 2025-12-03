namespace EfCoreLibraryAPI.DTO.Login.Request;

public class CreateLoginDto
{
    public string? Username { get; set; }
    public string? FullName { get; set; }
    public string? Password { get; set; }
}