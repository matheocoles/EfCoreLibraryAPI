using FastEndpoints;

namespace EfCoreLibraryAPI.DTO.Actor.Request;

public class CreateAuthorDto :IMapper
{
    public string? Name { get; set; }
    public string? FirstName { get; set; }
}