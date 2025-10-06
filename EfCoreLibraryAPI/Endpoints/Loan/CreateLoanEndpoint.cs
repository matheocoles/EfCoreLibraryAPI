using EfCoreLibraryAPI.DTO.Loan.Request;
using EfCoreLibraryAPI.DTO.Loan.Response;
using FastEndpoints;

namespace EfCoreLibraryAPI.Endpoints.Loan;

public class CreateLoanEndpoint(LibraryDbContext libraryDbContext) : Endpoint<CreateLoanDto, GetLoanDto>
{
    public override void Configure()
    {
        Post(("/api/loans"));
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateLoanDto req, CancellationToken ct)
    {
        Models.Loan loan = new ()
        {
            Date = req.Date,
            PlannedReturningDate = req.PlannedReturningDate,
            EffectiveReturningDate = req.EffectiveReturningDate
        };
        
        libraryDbContext.Loans.Add(loan);
        await libraryDbContext.SaveChangesAsync(ct);
        
        Console.WriteLine("Empreint créé avec succès !");

        GetLoanDto responseDto = new ()
        {
            Id = loan.Id,
            Date = req.Date,
            PlannedReturningDate = req.PlannedReturningDate,
            EffectiveReturningDate = req.EffectiveReturningDate
        };

        await Send.OkAsync(responseDto, ct);
    }
}