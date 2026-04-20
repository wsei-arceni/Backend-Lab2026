using AppCore.Interfaces;
using AppCore.Models;
using AppCore.ValueObjects;

namespace Infrastructure.Memory;

public class MemoryPersonRepository: MemoryGenericRepository<Person>, IPersonRepository
{
    public Task<IEnumerable<Person>> GetByEmployerAsync(Guid companyId)
    {
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
        throw new NotImplementedException();
    }

    public List<Person> GetPersonsByOrganization(Organization organization)
    {
        return _data.Values.Where(p => p.Organization == organization).ToList();
    }

    public MemoryPersonRepository() : base()
    {
        var id1 = Guid.NewGuid();
        _data.Add(id1, new Person()
        {
            Id = id1,
            FirstName = "Zoe",
            LastName = "Koval",
            Gender = Gender.Female,
        });
        var id2 = Guid.NewGuid();
        _data.Add(id2, new Person()
        {
            Id = id2,
            FirstName = "Mike",
            LastName = "Schmidt",
            Gender = Gender.Male,
        });
    }
}