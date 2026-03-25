using AppCore.Interfaces;
using AppCore.Models;
using AppCore.ValueObjects;

namespace Infrastructure.Memory;

public class MemoryPersonRepository: MemoryGenericRepository<Person>, IPersonRepository
{
    public Task<IEnumerable<Person>> GetByEmployerAsync(Guid companyId)
    {
        /* TODO: Person don't have Employer field (Lab 3) */
        return Task.FromResult(_data.Values.Where(p => p.Employer != null && p.Employer.Id == companyId));
    }

    public Task<IEnumerable<Person>> GetByOrganizationAsync(Guid organizationId)
    {
        return Task.FromResult(_data.Values.Where(p => p.Organization != null && p.Organization.Id == organizationId));
    }

    public Task<IEnumerable<Person>> SearchAsync(string query)
    {
        return Task.FromResult(_data.Values.Where(p => p.FirstName.Contains(query) || p.LastName.Contains(query)));
    }

    public List<Person> GetPersonsByCompany(Company company)
    {
        return _data.Values.Where(p => p.Company == company).ToList();
    }

    public List<Person> GetPersonsByOrganization(Organization organization)
    {
        return _data.Values.Where(p => p.Organization == organization).ToList();
    }

    public MemoryPersonRepository() : base()
    {
        _data.Add(Guid.NewGuid(), new Person()
        {
            FirstName = "Zoe",
            LastName = "Koval",
            Gender = Gender.Female,
        });
        _data.Add(Guid.NewGuid(), new Person()
        {
            FirstName = "Mike",
            LastName = "Schmidt",
            Gender = Gender.Male,
        });
    }
}