using AppCore.Interfaces;

namespace AppCore.Dto;

public record CrmUserDto
{
    public string Id { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string FullName { get; init; } = string.Empty;
    public string Department { get; init; } = string.Empty;
    public IList<string> Roles { get; init; }
    public SystemUserStatus Status { get; init; }
    public DateTime CreatedAt { get; init; }
}
