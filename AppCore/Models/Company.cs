namespace AppCore.Models;

public class Company: Contact
{
    public string Name { get; set; }
    public string? NIP { get; set; }
    public string? REGION { get; set; }
    public string? KRS { get; set; }
    public string? Industry { get; set; }
    public int? EmployeeCount { get; set; }
    public decimal? AnnualRevenue { get; set; }
    public string? Website { get; set; }
    public List<Person> Employees { get; set; }
    public Person? PrimaryContact { get; set; }

    public string GetDisplayName()
    {
        throw new NotImplementedException();
    }
}