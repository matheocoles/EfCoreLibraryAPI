namespace EfCoreLibraryAPI.DTO.Loan.Request;

public class PatchEffectiveReturnDto
{
    public int Id {get; set;}
    public DateOnly EffectiveReturningDate { get; set; }
}