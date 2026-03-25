using AppCore.ValueObjects;

namespace AppCore.Models;

public class Person: Contact
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public DateTime? BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string? Position { get; set; }
    public Organization? Organization { get; set; }
    public Company Company { get; set; }

    public string GetDisplayName()
    {
        return $"{FirstName} {MiddleName} {LastName}";
    }
}