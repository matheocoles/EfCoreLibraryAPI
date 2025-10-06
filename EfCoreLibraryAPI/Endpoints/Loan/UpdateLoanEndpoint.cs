using EfCoreLibraryAPI.DTO.Actor.Request;
using EfCoreLibraryAPI.DTO.Actor.Response;
using EfCoreLibraryAPI.DTO.Loan.Request;
using EfCoreLibraryAPI.DTO.Loan.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.Loan;

public class UpdateLoanEndpoint(LibraryDbContext libraryDbContext) :Endpoint<UpdateLoanDto, GetLoanDto>
{
    public override void Configure()
    {
        Put("/api/loans/{@id}", x => new { x.Id });
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateLoanDto req, CancellationToken ct)
    {
        Models.Loan? loanToEdit = await libraryDbContext
            .Loans
            .SingleOrDefaultAsync(l => l.Id == req.Id, cancellationToken: ct);

        if (loanToEdit == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        loanToEdit.Date = req.Date;
        loanToEdit.PlannedReturningDate = req.PlannedReturningDate;
        loanToEdit.EffectiveReturningDate = req.EffectiveReturningDate;

        await libraryDbContext.SaveChangesAsync(ct);

        GetLoanDto responseDto = new()
        {
            Id = req.Id,
            Date = req.Date,
            PlannedReturningDate = req.PlannedReturningDate,
            EffectiveReturningDate = req.EffectiveReturningDate,
        };

        await Send.OkAsync(responseDto, ct);
    }
}
