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
    public static PersonDto FromPerson(Person person) => new()
    {
        FirstName = person.FirstName,
        LastName = person.LastName,
        Position = person.Position,
        BirthDate = person.BirthDate,
        Gender = person.Gender,
        Notes = new List<NoteDto>()
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
            Phone = Phone,
            BirthDate = BirthDate,
            Address = new Address()
            {
                City = Address?.City,
                Street = Address?.Street,
                PostalCode = Address?.PostalCode,
                Country = Address?.Country,
            }
        };
    }
};

public record UpdatePersonDto(
    Guid Id,
    string? FirstName,
    string? LastName,
    string? Email,
    string? Phone,
    string? Position,
    DateTime? BirthDate,
    Gender? Gender,
    AddressDto? Address,
    ContactStatus? Status
)
{
    public void UpdateEntity(Person current)
    {
        if (FirstName != null) current.FirstName = FirstName;
        if (LastName != null) current.LastName = LastName;
        if (Email != null) current.Email = Email;
        if (Phone != null) current.Phone = Phone;
        if (Position != null) current.Position = Position;
        if (BirthDate.HasValue) current.BirthDate = BirthDate;
        if (Gender.HasValue) current.Gender = Gender.Value;
        if (Status.HasValue) current.Status = Status.Value;
        
        if (Address != null)
        {
            current.Address = new Address()
            {
                City = Address.City,
                Street = Address.Street,
                PostalCode = Address.PostalCode,
                Country = Address.Country,
                Type = Address.Type
            };
        }
    }

};