using AppCore.Interfaces;
using AppCore.Models;

namespace Infrastructure.Memory;

public class MemoryCompanyRepository: MemoryGenericRepository<Company>, ICompanyRepository
{
    public List<Company> GetCompaniesByName(string name)
    {
        throw new NotImplementedException();
    }

    public Company GetCompanyByNIP(string nip)
    {
        throw new NotImplementedException();
    }

    public List<Person> GetCompanyMembers(Company company)
    {
        throw new NotImplementedException();
    }
}