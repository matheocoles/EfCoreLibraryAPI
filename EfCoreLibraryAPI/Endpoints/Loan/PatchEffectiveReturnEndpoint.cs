using EfCoreLibraryAPI.DTO.Loan.Request;
using EfCoreLibraryAPI.DTO.Loan.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.Loan;

public class PatchEffectiveReturnEndpoint(LibraryDbContext libraryDbContext) : Endpoint<PatchEffectiveReturnDto, GetLoanDto>
{
    public override void Configure()
    {
        Patch("/api/loans/{@id}/effectivereturn", x => new {x.Id}) ;
        AllowAnonymous();
    }

    public override async Task HandleAsync(PatchEffectiveReturnDto req, CancellationToken ct)
    {
        
        
        Models.Loan? loan = await libraryDbContext
            .Loans
            .Include(l => l.Book)
            .Include(l => l.User)
            .SingleOrDefaultAsync(l => l.Id == req.Id , ct);

        if (loan == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        loan.EffectiveReturningDate = req.EffectiveReturningDate;

        await libraryDbContext.SaveChangesAsync(ct);

        var response = new GetLoanDto
        {
            Id = loan.Id,
            Date = loan.Date,
            PlannedReturningDate = loan.PlannedReturningDate,
            EffectiveReturningDate = loan.EffectiveReturningDate,
            BookId = loan.BookId,
            BookTitle = loan.Book?.Title,
            BookReleaseYear = loan.Book.ReleaseYear,
            BookIsbn = loan.Book.Isbn,
            UserId = loan.UserId,
            UserName = loan.User?.Name,
            UserFirstName = loan.User?.FirstName,
            UserEmail = loan.User?.Email,
            UserBirthday = loan.User.Birthday
        };

        await Send.OkAsync(response, ct);
    }
}