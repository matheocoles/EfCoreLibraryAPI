using EfCoreLibraryAPI.DTO.Book.Response;
using EfCoreLibraryAPI.DTO.Loan.Request;
using EfCoreLibraryAPI.DTO.Loan.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

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
        var book = await libraryDbContext.Books
            .FirstOrDefaultAsync(b => b.Id == req.BookId, ct);

        if (book == null)
        {
            await Send.NotFoundAsync();
            return;
        }
        
        var user = await libraryDbContext.Users
            .SingleOrDefaultAsync(b => b.Id == req.UserId, ct);
        
        if (user == null)
        {
            await Send.NotFoundAsync();
            return;
        }
        
        var plannedReturningDate = req.Date.AddMonths(2);
        
        Models.Loan loan = new ()
        {
            Date = req.Date,
            PlannedReturningDate = plannedReturningDate,
            BookId = req.BookId,
            UserId = req.UserId
        };
        
        
        libraryDbContext.Loans.Add(loan);
        await libraryDbContext.SaveChangesAsync(ct);
        
        Console.WriteLine("Empreint créé avec succès !");
        
        
        var responseDto = new GetLoanDto
        {
            Id = loan.Id ,
            Date = loan.Date,
            PlannedReturningDate = plannedReturningDate,
            BookId = book.Id,
            BookTitle = book.Title,
            BookReleaseYear = book.ReleaseYear,
            BookIsbn = book.Isbn,
            UserId = user.Id,
            UserName = user.Name,
            UserFirstName = user.FirstName,
            UserEmail = user.Email,
            UserBirthday = user.Birthday
        };

        await Send.OkAsync(responseDto, ct);
    }
}