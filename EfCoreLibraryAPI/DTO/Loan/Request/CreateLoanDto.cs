namespace EfCoreLibraryAPI.DTO.Loan.Request;

public class CreateLoanDto
{
    public DateOnly Date {get; set;}
    public int BookId { get; set; }
    public int UserId {get; set; }
}