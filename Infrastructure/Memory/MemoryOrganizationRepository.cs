using AppCore.Interfaces;
using AppCore.Models;
using AppCore.ValueObjects;

namespace Infrastructure.Memory;

public class MemoryOrganizationRepository: MemoryGenericRepository<Organization>, IOrganizationRepository
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