namespace EfCoreLibraryAPI.DTO.Book.Request;

public class CreateBookDto
{
    public string? Title {get; set;}
    public int ReleaseYear {get; set;}
    public string? Isbn {get; set;}
    public int AuthorId {get; set;}
    public string? AuthorName {get; set;}
    public string? AuthorFirstName {get; set;}
    
    
}