namespace EfCoreLibraryAPI.DTO.Loan.Request;

public class CreateLoanDto
{
    public DateOnly Date {get; set;}
    public DateOnly PlannedReturningDate {get; set;}
    public DateOnly EffectiveReturningDate {get; set;}
}