using AppCore.Interfaces;
using AppCore.Models;
using Infrastructure.EntityFramework.Context;

namespace Infrastructure.EntityFramework.Repositories;

public class EfPersonRepository(ContactsDbContext context): EfGenericRepository<Person>(context.Persons), IPersonRepository

{
    public List<Person> GetPersonsByCompany(Company company)
    {
        throw new NotImplementedException();
    }

    public List<Person> GetPersonsByOrganization(Organization organization)
    {
        throw new NotImplementedException();
    }
}