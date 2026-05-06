using AppCore.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.EntityFramework.Entities;

public class CrmUser: IdentityUser, ISystemUser
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string FullName { get; set; }
    public override string Email { get; set; } = string.Empty;
    public required string Department { get; set; }
    public UserRole Role { get; set; }
    public required SystemUserStatus Status { get; set; }
    public DateTime CreatedAt { get; set;  }
    public DateTime? LastLoginAt { get; private set; }
    public DateTime? DeactivatedAt { get; private set; }
    
    public void Activate()
    {
        if (Status == SystemUserStatus.Inactive) Status = SystemUserStatus.Active;
    }

    public void Deactivate(DateTime now)
    {
        if (Status != SystemUserStatus.Active) return;
        Status = SystemUserStatus.Inactive;
        DeactivatedAt = now;
    }
}

public class CrmRole : IdentityRole
{
    public string? Description { get; set; }

    public CrmRole() { }
    public CrmRole(string roleName, string? description = null)
        : base(roleName)
    {
        Description = description;
    }
}