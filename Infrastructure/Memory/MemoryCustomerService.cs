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
            Id = new Guid("516A34D7-CCFB-4F20-85F3-62BD0F3AF270"),
            FirstName = "Joey",
            LastName = "Drew",
            Email = "Jo.D@mail.com",
            Phone = "123456789",
            AddressId = new Guid("516A34D7-CCFB-4F20-85F3-62BD0F3AF271")
        },
        new Customer()
        {
            Id = new Guid("516A34D7-CCFB-4F20-85F3-62BD0F3AF271"),
            FirstName = "William",
            LastName = "Afton",
            Email = "Will.Afton@mail.com",
            Phone = "987654321",
            AddressId = new Guid("516A34D7-CCFB-4F20-85F3-62BD0F3AF271")
        }
        ];
    }

    public Task<IEnumerable<Customer>> GetCustomersAsync()
    {
        return Task.FromResult(GetCustomers());
    }
}