namespace EfCoreLibraryAPI.DTO.Loan.Response;

public class GetLoanDto
{
    public int Id { get; set; }
    public DateOnly Date {get; set;}
    public DateOnly PlannedReturningDate {get; set;}
    public DateOnly EffectiveReturningDate {get; set;}
}