using AppCore.Interfaces;
using AppCore.Models;
using AppCore.ValueObjects;
using Infrastructure.EntityFramework.Context;

namespace Infrastructure.EntityFramework.Repositories;

public class EfOrganizationRepository(ContactsDbContext context): EfGenericRepository<Organization>(context.Organizations), IOrganizationRepository
{
    public List<Organization> GetOrganizationByType(OrganizationType type)
    {
        throw new NotImplementedException();
    }

    public List<Person> GetOrganizationMembers(Organization organization)
    {
        throw new NotImplementedException();
    }
}
