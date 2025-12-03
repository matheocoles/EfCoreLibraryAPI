namespace ApiEfCoreLibrary.DTO.Login.Response;

public class GetLoginDto
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? FullName { get; set; }
    public string? Password { get; set; }
    public string? Salt { get; set; }
}