namespace EfCoreLibraryAPI.DTO.User.Request;

public class CreateUserDto
{
    public string? Name { get; set; }
    public string? Firstname { get; set; }
    public string? Email { get; set; }
    public DateOnly? Birthday { get; set; }
    
}