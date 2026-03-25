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

/* TODO: Is Contact have to be abstract. Or have to be created ContactBase and used instead Contact (supposed) */
public static ContactBaseDto FromEntity(Contact contact) => new()
{
    Id = contact.Id,
    Email = contact.Email,
    Phone = contact.Phone,
    Address = contact.Address,
    Status = contact.Status,
    Tags = contact.Tags,
    CreatedAt = contact.CreatedAt
};

public static Contact ToEntity(CreatePersonDto dto, Guid id) => new(id)
{
    Email = dto.Email,
    Phone = dto.Phone,
    Address = dto.Address,
    Status = dto.Status,
    Tags = dto.Tags,
    CreatedAt = dto.CretedAt
};
}

public record AddressDto(
string Street,
string City,
string PostalCode,
string Country,
AddressType Type
);