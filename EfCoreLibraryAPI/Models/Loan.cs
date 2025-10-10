using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using EfCoreLibraryAPI.Models;

namespace EfCoreLibraryAPI.Models;

public class Loan
{
    [Key] public int Id { get; set; }
    [Required] public DateOnly? Date { get; set; }
    [Required] public DateOnly? PlannedReturningDate { get; set; }
    public DateOnly? EffectiveReturningDate { get; set; }
    
    [Required] public int BookId { get; set; }
    public Book? Book { get; set; }
    public User? User { get; set; }
    [Required] public int UserId { get; set; }
}