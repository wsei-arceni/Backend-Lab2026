using AppCore.Models;
using AppCore.ValueObjects;

namespace AppCore.Dto;

public record PersonDto : ContactBaseDto
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string? Position { get; init; }
    public DateTime? BirthDate { get; init; }
    public Gender Gender { get; init; }
    public Guid? EmployerId { get; init; }

    public static PersonDto FromPerson(Person person) => new PersonDto
    {
        FirstName = person.FirstName,
        LastName = person.LastName,
    };
}

public record CreatePersonDto(
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string? Position,
    DateTime? BirthDate,
    Gender Gender,
    Guid? EmployerId,
    AddressDto? Address
)
{
    public Person ToEntity()
    {
        return new Person
        {
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            Address = new Address()
            {
                City = Address?.City,
                Street = Address?.Street,
                PostalCode = Address?.PostalCode,
                Country = Address?.Country,
            }
        };
    }
} ;

public record UpdatePersonDto(
    string? FirstName,
    string? LastName,
    string? Email,
    string? Phone,
    string? Position,
    DateTime? BirthDate,
    Gender? Gender,
    Guid? EmployerId,
    AddressDto? Address,
    ContactStatus? Status
);