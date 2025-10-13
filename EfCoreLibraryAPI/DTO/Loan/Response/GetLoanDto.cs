namespace EfCoreLibraryAPI.DTO.Loan.Response;

public class GetLoanDto
{
    public int Id { get; set; }
    public DateOnly? Date { get; set; }
    public DateOnly? PlannedReturningDate { get; set; }
    public DateOnly? EffectiveReturningDate { get; set; }
    public int BookId { get; set; }
    public string? BookTitle { get; set; }
    public int BookReleaseYear { get; set; }
    public string? BookIsbn { get; set; }
    public string? BookAuthorName { get; set; }
    public string? BookAuthorFirstName { get; set; }
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string? UserFirstName { get; set; }
    public string? UserEmail { get; set; }
    public DateOnly UserBirthday { get; set; }
}