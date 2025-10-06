using FastEndpoints;

namespace EfCoreLibraryAPI.DTO.Actor.Response;

public class GetAuthorDto :IMapper
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? FirstName { get; set; }
}