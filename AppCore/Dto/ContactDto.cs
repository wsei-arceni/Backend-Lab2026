using AppCore.Models;
using AppCore.ValueObjects;

namespace AppCore.Dto;

public abstract record ContactBaseDto
{
    public Guid Id { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }
    public AddressDto Address { get; init; }
    public ContactStatus Status { get; init; }
    public List<string> Tags { get; init; } = new();
    public DateTime CreatedAt { get; init; }

/* TODO: Is Contact have to be abstract. Or have to be created ContactBase and used instead Contact (supposed) (Lab 3) */
/* TODO: Create mappings for rest classes (Lab 3) */
}

public record AddressDto(
string Street,
string City,
string PostalCode,
string Country,
AddressType Type
);