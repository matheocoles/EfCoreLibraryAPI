namespace EfCoreLibraryAPI.DTO.Dashboard;

public class RecentLoanDto
{
    public string BookTitle { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public DateOnly? LoanDate { get; set; }
}

public class DashboardStatsResponse
{
    public int TotalBooks { get; set; }
    public int TotalAuthors { get; set; }
    public int TotalUsers { get; set; }
    public int TotalLoans { get; set; }
    
    // Ajout de la liste des emprunts
    public List<RecentLoanDto> RecentLoans { get; set; } = new();
}