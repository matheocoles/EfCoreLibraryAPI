using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.Loan;

public class DeleteLoanRequest
{
    public int Id {get; set;}
}

public class DeleteLoanEndpoint(LibraryDbContext libraryDbContext) : Endpoint<DeleteLoanRequest>
{
    public override void Configure()
    {
        Delete("/api/loans/{@id}", x => new {x.Id});
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteLoanRequest req, CancellationToken ct)
    {
        Models.Loan? loanToDelete = await libraryDbContext
            .Loans
            .SingleOrDefaultAsync(l => l.Id == req.Id, cancellationToken: ct);

        if (loanToDelete == null)
        {
            Console.WriteLine($"Aucun emprunt avec l'id {req.Id} trouvé.");
            await Send.NotFoundAsync(ct);
            return;
        }
        
        libraryDbContext.Loans.Remove(loanToDelete);
        await libraryDbContext.SaveChangesAsync(ct);
        
        await Send.NotFoundAsync(ct);
    }
}