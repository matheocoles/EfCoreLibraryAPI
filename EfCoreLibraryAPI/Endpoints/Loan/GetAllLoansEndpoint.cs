using EfCoreLibraryAPI.DTO.Loan.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.Loan;

public class GetAllLoansEndpoint(LibraryDbContext libraryDbContext) : EndpointWithoutRequest<List<GetLoanDto>>
{
    public override void Configure()
    {
        Get("/api/loans");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        List<GetLoanDto> responseDto = await libraryDbContext
            .Loans
            .Include(l => l.Book)
            .Include(l => l.User)
            .Select(l => new GetLoanDto
            {
                Id = l.Id,
                Date = l.Date,
                PlannedReturningDate = l.PlannedReturningDate,
                EffectiveReturningDate = l.EffectiveReturningDate,
                BookId = l.BookId,
                BookTitle = l.Book.Title,
                BookReleaseYear = l.Book.ReleaseYear,
                BookIsbn = l.Book.Isbn,
                UserId = l.UserId,
                UserName = l.User.Name,
                UserFirstName = l.User.FirstName,
                UserEmail = l.User.Email,
                UserBirthday = l.User.Birthday
            }).ToListAsync(ct);

        await Send.OkAsync(responseDto, ct);
    }
}