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
            FirstName = "Joey",
            LastName = "Drew",
            Email = "Jo.D@mail.com",
            Phone = "123456789",
            AddressId = 1
        },
        new Customer()
        {
            Id = 2,
            FirstName = "William",
            LastName = "Afton",
            Email = "Will.Afton@mail.com",
            Phone = "987654321",
            AddressId = 2
        }
        ];
    }

    public Task<IEnumerable<Customer>> GetCustomersAsync()
    {
        return Task.FromResult(GetCustomers());
    }
}