namespace EfCoreLibraryAPI.DTO.Login.Request;

public class UpdateLoginDto
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? FullName { get; set; }
    public string? Password { get; set; }
}