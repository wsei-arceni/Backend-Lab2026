using AppCore.Models;
using AppCore.ValueObjects;

namespace AppCore.Interfaces;

public interface IOrganizationRepository: IGenericRepositoryAsync<Organization>
{
    public List<Organization> GetOrganizationByType(OrganizationType type);
    public List<Person> GetOrganizationMembers(Organization organization);
}