namespace AppCore.Models;

public class Customer: Contact
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required Guid AddressId { get; set; }
}