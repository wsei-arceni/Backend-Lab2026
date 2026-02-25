using AppCore.Interfaces;
using AppCore.Models;

namespace Infrastructure.Memory;

public class MemoryCustomerService: ICustomerService
{
    public IEnumerable<Customer> GetCustomers()
    {
        return
        [
        new Customer()
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "Jo.D@mail.com",
            Phone = "123456789",
            AddressId = 1
        },
        new Customer()
        {
            Id = 2,
            FirstName = "Jane",
            LastName = "Doe",
            Email = "Ja.D@mail.com",
            Phone = "987654321",
            AddressId = 2
        }
        ];
    }

    public Task<IEnumerable<Customer>> GetCustomersAsync()
    {
        throw new NotImplementedException();
    }
}