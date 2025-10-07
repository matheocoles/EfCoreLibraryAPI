namespace EfCoreLibraryAPI.DTO.Book.Request;

public class UpdateBookDto
{
    public int Id {get; set;}
    public string? Title {get; set;}
    public int? ReleaseYear {get; set;}
    public string? ISBN {get; set;}
    public int AuthorId {get; set;}
    public string? AuthorName {get; set;}
    public string? AuthorFirstName {get; set;}
}