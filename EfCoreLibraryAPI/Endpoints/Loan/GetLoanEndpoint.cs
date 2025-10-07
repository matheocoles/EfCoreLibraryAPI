using EfCoreLibraryAPI.DTO.Loan.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.Loan;

public class GetLoanRequest
{
    public int Id { get; set; }
}

public class GetLoanEndpoint(LibraryDbContext libraryDbContext) : Endpoint<GetLoanRequest, GetLoanDto>
{
    public override void Configure()
    {
        Get("/api/loans/{@id}", x => new {x.Id});
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetLoanRequest req, CancellationToken ct)
    {
        Models.Loan? loan = await libraryDbContext
            .Loans
            .SingleOrDefaultAsync(l => l.Id == req.Id, cancellationToken: ct);

        if (loan == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        GetLoanDto responseDto = new()
        {
            Id = req.Id,
            Date = loan.Date,
            PlannedReturningDate = loan.PlannedReturningDate,
            EffectiveReturningDate = loan.EffectiveReturningDate,
            BookId = loan.BookId,
            UserId = loan.UserId
        };

        await Send.OkAsync(responseDto, ct);
    }
}