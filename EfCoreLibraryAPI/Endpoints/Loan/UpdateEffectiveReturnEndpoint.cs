using EfCoreLibraryAPI.DTO.Loan.Request;
using EfCoreLibraryAPI.DTO.Loan.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.Loan;

public class UpdateEffectiveReturnEndpoint(LibraryDbContext libraryDbContext) : Endpoint<UpdateEffectiveReturnDto, GetLoanDto>
{
    public override void Configure()
    {
        Patch("/api/loans/{id}/effectivereturn");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateEffectiveReturnDto req, CancellationToken ct)
    {
        var id = Route<GetLoanDto>(null, false).Id;
        
        var loan = await libraryDbContext.Loans
            .FirstOrDefaultAsync(l => l.Id == id , ct);

        if (loan == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        // Mise à jour de la date de retour effective
        loan.EffectiveReturningDate = req.EffectiveReturningDate;

        await libraryDbContext.SaveChangesAsync(ct);

        var response = new GetLoanDto
        {
            Id = loan.Id,
            Date = loan.Date,
            PlannedReturningDate = loan.PlannedReturningDate,
            EffectiveReturningDate = loan.EffectiveReturningDate,
            BookId = loan.BookId,
            UserId = loan.UserId
        };

        await Send.OkAsync(response, ct);
    }
}