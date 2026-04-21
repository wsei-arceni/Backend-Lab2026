using AppCore.Interfaces;
using AppCore.Models;
using Infrastructure.EntityFramework.Context;

namespace Infrastructure.EntityFramework.Repositories;

public class EfCompanyRepository(ContactsDbContext context): EfGenericRepository<Company>(context.Companies), ICompanyRepository
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