namespace EfCoreLibraryAPI.DTO.User.Request;

public class UpdateUserDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? FirstName { get; set; }
    public string? Email { get; set; }
    public DateOnly? Birthday { get; set; }
}