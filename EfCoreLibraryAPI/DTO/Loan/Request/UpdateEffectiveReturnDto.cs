namespace EfCoreLibraryAPI.DTO.Loan.Request;

public class UpdateEffectiveReturnDto
{
    public int Id {get; set;}
    public DateOnly EffectiveReturningDate { get; set; }
}