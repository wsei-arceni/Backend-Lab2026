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
    public static PersonDto FromPerson(Person person) => new()
    {
        Id = person.Id,
        FirstName = person.FirstName,
        LastName = person.LastName,
        Email = person.Email,
        Phone = person.Phone,
        Position = person.Position,
        BirthDate = person.BirthDate,
        Gender = person.Gender,
        Status = person.Status,
        Address = person.Address != null ? new AddressDto(
            person.Address.Street,
            person.Address.City,
            person.Address.PostalCode,
            person.Address.Country,
            person.Address.Type
        ) : null,
        Notes = person.Notes?.Select(NoteDto.FromNote).ToList() ?? new List<NoteDto>()
    };
}

public record CreatePersonDto(
    string FirstName,
    string LastName,
    string MiddleName,
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
            MiddleName = MiddleName,
            Email = Email,
            Phone = Phone,
            BirthDate = BirthDate,
            Address = new Address()
            {
                City = Address?.City ?? string.Empty,
                Street = Address?.Street ?? string.Empty,
                PostalCode = Address?.PostalCode ?? string.Empty,
                Country = Address?.Country ?? string.Empty,
            }
        };
    }
};

public record UpdatePersonDto(
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