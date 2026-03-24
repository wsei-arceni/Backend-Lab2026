using AppCore.Models;

namespace AppCore.Interfaces;

public interface ICompanyRepository: IGenericRepositoryAsync<Company>
{
    public List<Company> GetCompaniesByName(string name);
    public Company GetCompanyByNIP(string nip);
    public List<Person> GetCompanyMembers(Company company);
}