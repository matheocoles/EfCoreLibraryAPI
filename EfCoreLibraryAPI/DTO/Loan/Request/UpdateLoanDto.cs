namespace EfCoreLibraryAPI.DTO.Loan.Request;

public class UpdateLoanDto
{
    public int Id { get; set; }
    public DateOnly Date {get; set;}
    public DateOnly PlannedReturningDate {get; set;}
    public DateOnly EffectiveReturningDate {get; set;}
}