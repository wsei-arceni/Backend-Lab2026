using AppCore.Interfaces;
using AppCore.Models;

namespace Infrastructure.Memory;

public class MemoryCompanyRepository: MemoryGenericRepository<Company>, ICompanyRepository
{
    public List<Company> GetCompaniesByName(string name)
    {
        return _data.Values.Where(c => c.Name.Contains(name)).ToList();
    }

    public Company GetCompanyByNIP(string nip)
    {
        return _data.Values.Where(c => c.NIP == nip).FirstOrDefault();
    }

    public List<Person> GetCompanyMembers(Company company)
    {
        return _data[company.Id].Employees;
    }
}