namespace EfCoreLibraryAPI.DTO.Book.Response;

public class GetBookDto
{
    public int Id {get; set;}
    public string? Title {get; set;}
    public int? ReleaseYear {get; set;}
    public string? ISBN {get; set;}
    public int AuthorId {get; set;}
}