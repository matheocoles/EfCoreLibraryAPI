namespace EfCoreLibraryAPI.DTO.User.Response;

public class GetUserDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? FirstName { get; set; }
    public string? Email { get; set; }
    public DateOnly? Birthday { get; set; }
}