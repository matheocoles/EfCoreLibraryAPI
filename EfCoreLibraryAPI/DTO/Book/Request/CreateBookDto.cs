namespace EfCoreLibraryAPI.DTO.Book.Request;

public class CreateBookDto
{
    public string? Title {get; set;}
    public int? ReleaseYear {get; set;}
    public string? ISBN {get; set;}
    
}