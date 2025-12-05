using EfCoreLibraryAPI.DTO.Dashboard;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.Dashboard;

public class GetDashboardStatsEndpoint(LibraryDbContext database) : EndpointWithoutRequest<DashboardStatsResponse>
{
    public override void Configure()
    {
        Get("/dashboard/stats");
        Roles("Admin", "User");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        // 1. Les Compteurs (inchangés)
        var booksCount = await database.Books.CountAsync(ct);
        var authorsCount = await database.Authors.CountAsync(ct);
        var usersCount = await database.Users.CountAsync(ct);
        var loansCount = await database.Loans.CountAsync(ct);

        
        var recentLoans = await database.Loans
            .OrderByDescending(loan => loan.Date) 
            .Take(5)
            .Select(loan => new RecentLoanDto
            {
                BookTitle = loan.Book.Title,       
                UserName = loan.User.Name,    
                LoanDate = loan.Date
            })
            .ToListAsync(ct);

        // 3. Réponse complète
        var response = new DashboardStatsResponse
        {
            TotalBooks = booksCount,
            TotalAuthors = authorsCount,
            TotalUsers = usersCount,
            TotalLoans = loansCount,
            RecentLoans = recentLoans // <--- On remplit la liste
        };

        await Send.OkAsync(response, ct);
    }
}