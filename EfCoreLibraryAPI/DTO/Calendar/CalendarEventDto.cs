namespace EfCoreLibraryAPI.DTO.Calendar;

public class CalendarEventDto
{
    public DateTime Date { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Type { get; set; } = "success"; // "success", "warning", "error"
}