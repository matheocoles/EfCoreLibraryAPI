using EfCoreLibraryAPI.DTO.Book.Response;
using EfCoreLibraryAPI.DTO.Loan.Request;
using EfCoreLibraryAPI.DTO.Loan.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.Loan;

public class CreateLoanEndpoint(LibraryDbContext libraryDbContext) : Endpoint<CreateLoanDto, GetLoanDto, GetBookDto>
{
    public override void Configure()
    {
        Post(("/api/loans"));
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateLoanDto req, CancellationToken ct)
    {
        List<Models.Book> book = await libraryDbContext
            .Books
            .Select(b => new Models.Book { Id = b.Id, Title = b.Title })
            .ToListAsync();
        
        if (book == null)
        {
            await Send.NotFoundAsync();
            return;
        }
        
        List<Models.User> user = await libraryDbContext
            .Users
            .Select(u => new Models.User { Id = u.Id, Name = u.Name, FirstName = u.FirstName})
            .ToListAsync();
        
        if (user == null)
        {
            await Send.NotFoundAsync();
            return;
        }
        
        var plannedReturningDate = req.Date.AddMonths(2);
        
        Models.Loan loan = new ()
        {
            Date = req.Date,
            //PlannedReturningDate = req.PlannedReturningDate,
            BookId = req.BookId,
            UserId = req.UserId
        };
        
        libraryDbContext.Loans.Add(loan);
        await libraryDbContext.SaveChangesAsync(ct);
        
        Console.WriteLine("Empreint créé avec succès !");

        GetLoanDto responseDto = new ()
        {
            Id = loan.Id,
            Date = req.Date,
            PlannedReturningDate = plannedReturningDate ,
            BookId = req.BookId,
            UserId = req.UserId
        };

        await Send.OkAsync(responseDto, ct);
    }
}