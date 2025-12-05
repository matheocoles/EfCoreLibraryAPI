using EfCoreLibraryAPI.DTO.Calendar;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLibraryAPI.Endpoints.Calendar;

public class GetCalendarEventsEndpoint(LibraryDbContext database) : EndpointWithoutRequest<List<CalendarEventDto>>
{
    public override void Configure()
    {
        Get("/calendar/events");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);
        DateOnly warningThreshold = today.AddDays(3);

        var loans = await database.Loans
            .Include(l => l.Book)
            .Include(l => l.User)
            .Where(l => l.PlannedReturningDate != null)
            .Select(l => new 
            {
                l.PlannedReturningDate,
                l.EffectiveReturningDate, // <--- AJOUT IMPORTANT
                BookTitle = l.Book.Title,
                UserName = l.User.Name
            })
            .ToListAsync(ct);

        var events = loans.Select(l => new CalendarEventDto
        {
            Date = l.PlannedReturningDate!.Value.ToDateTime(TimeOnly.MinValue),
            
            // On ajoute "(Rendu)" au titre si le livre est revenu
            Title = l.EffectiveReturningDate != null 
                ? $"✔ {l.BookTitle} ({l.UserName})" 
                : $"{l.BookTitle} ({l.UserName})",
            
            // Nouvelle logique de statut
            Type = GetStatus(l.PlannedReturningDate, l.EffectiveReturningDate, today, warningThreshold)
        }).ToList();

        await Send.OkAsync(events, ct);
    }

    private string GetStatus(DateOnly? dueDate, DateOnly? returnDate, DateOnly today, DateOnly threshold)
    {
        // 1. Si le livre est rendu, c'est VERT (Success), peu importe la date
        if (returnDate != null) return "success";

        if (dueDate is null) return "default";

        // 2. Si pas rendu et date passée -> ROUGE (Error)
        if (dueDate.Value < today) return "error";

        // 3. Si pas rendu et date proche (J-3) -> ORANGE (Warning)
        if (dueDate.Value <= threshold) return "warning";

        // 4. Sinon (pas rendu, mais dans le futur) -> BLEU (Processing)
        return "processing"; 
    }
}