using AppCore.Models;

namespace AppCore.Interfaces;

public interface IPersonRepository: IGenericRepositoryAsync<Person>
{
    public List<Person> GetPersonsByCompany(Company company);
    public List<Person> GetPersonsByOrganization(Organization organization);
}