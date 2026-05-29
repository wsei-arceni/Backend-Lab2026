using System.Text.Json;
using AppCore.Dto;
using AppCore.Interfaces;
using AppCore.Models;
using AppCore.ValueObjects;
using FluentValidation;

namespace AppCore.Services;

public class ContactImportService : IContactImportService
{
    private readonly IContactUnitOfWork _unitOfWork;
    private readonly IValidator<CreatePersonDto> _personValidator;

    public ContactImportService(
        IContactUnitOfWork unitOfWork,
        IValidator<CreatePersonDto> personValidator)
    {
        _unitOfWork = unitOfWork;
        _personValidator = personValidator;
    }

    public async Task<ImportResultDto> ImportFromJsonDtoAsync(ContactImportDto? importDto, string importedBy)
    {
        var result = new ImportResultDto();

        if (importDto == null) return result;
        
        if (importDto.People != null)
        {
            foreach (var personDto in importDto.People)
            {
                await ProcessPersonAsync(personDto, importedBy, result);
            }
        }

        if (importDto.Companies != null)
        {
            foreach (var companyDto in importDto.Companies)
            {
                await ProcessCompanyAsync(companyDto, importedBy, result);
            }
        }

        if (importDto.Organizations != null)
        {
            foreach (var orgDto in importDto.Organizations)
            {
                await ProcessOrganizationAsync(orgDto, importedBy, result);
            }
        }

        await _unitOfWork.SaveChangesAsync();

        return result;
    }

    public async Task<ImportResultDto> ImportFromCsvAsync(Stream csvStream, string importedBy)
    {
        var result = new ImportResultDto();
        using var reader = new StreamReader(csvStream);
        string? line;
        string? currentGroup = null;
        string[]? headers = null;

        while ((line = await reader.ReadLineAsync()) != null)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            if (line.Trim().Equals("People", StringComparison.OrdinalIgnoreCase) ||
                line.Trim().Equals("Companies", StringComparison.OrdinalIgnoreCase) ||
                line.Trim().Equals("Organizations", StringComparison.OrdinalIgnoreCase) ||
                line.Trim().Equals("Organisation", StringComparison.OrdinalIgnoreCase))
            {
                currentGroup = line.Trim();
                headers = null;
                continue;
            }

            if (currentGroup == null) continue;

            if (headers == null)
            {
                headers = SplitLine(line);
                continue;
            }

            var values = SplitLine(line);
            var data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < Math.Min(headers.Length, values.Length); i++)
            {
                data[headers[i]] = values[i];
            }

            await ProcessCsvRowAsync(currentGroup, data, importedBy, result);
        }

        await _unitOfWork.SaveChangesAsync();

        return result;
    }

    private string[] SplitLine(string line)
    {
        char? delimiter = null;
        foreach (var c in line)
        {
            if (!char.IsLetterOrDigit(c) && c != '@' && c != '+' && c != '.' && c != ' ' && c != '-')
            {
                delimiter = c;
                break;
            }
        }

        if (delimiter == null) return new[] { line.Trim() };

        return line.Split(delimiter.Value).Select(v => v.Trim()).ToArray();
    }

    private async Task ProcessCsvRowAsync(string group, Dictionary<string, string> data, string importedBy, ImportResultDto result)
    {
        try
        {
            if (group.Equals("People", StringComparison.OrdinalIgnoreCase))
            {
                var dto = new CreatePersonDto(
                    data.GetValueOrDefault("FirstName") ?? "",
                    data.GetValueOrDefault("LastName") ?? "",
                    data.GetValueOrDefault("MiddleName"),
                    data.GetValueOrDefault("Email") ?? "",
                    data.GetValueOrDefault("Phone") ?? "",
                    data.GetValueOrDefault("Position"),
                    DateTime.TryParse(data.GetValueOrDefault("BirthDate"), out var bd) ? bd : null,
                    Enum.TryParse<Gender>(data.GetValueOrDefault("Gender"), out var g) ? g : Gender.NotSpecified,
                    null,
                    null
                );
                await ProcessPersonAsync(dto, importedBy, result);
            }
            else if (group.Equals("Companies", StringComparison.OrdinalIgnoreCase))
            {
                var dto = new CreateCompanyDto(
                    data.GetValueOrDefault("Name") ?? "",
                    data.GetValueOrDefault("NIP"),
                    data.GetValueOrDefault("REGON"),
                    data.GetValueOrDefault("Email"),
                    data.GetValueOrDefault("Phone"),
                    null
                );
                await ProcessCompanyAsync(dto, importedBy, result);
            }
            else if (group.Equals("Organizations", StringComparison.OrdinalIgnoreCase) || group.Equals("Organisation", StringComparison.OrdinalIgnoreCase))
            {
                var dto = new CreateOrganizationDto(
                    data.GetValueOrDefault("Name") ?? "",
                    data.GetValueOrDefault("Email"),
                    data.GetValueOrDefault("Phone"),
                    null
                );
                await ProcessOrganizationAsync(dto, importedBy, result);
            }
        }
        catch (Exception ex)
        {
            result.Errors.Add(new ImportErrorReportDto(string.Join(",", data.Values), new List<string> { ex.Message }));
        }
    }

    private async Task ProcessPersonAsync(CreatePersonDto dto, string importedBy, ImportResultDto result)
    {
        var validationResult = await _personValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            result.Errors.Add(new ImportErrorReportDto($"{dto.FirstName} {dto.LastName}", validationResult.Errors.Select(e => e.ErrorMessage).ToList()));
            return;
        }

        var existing = await _unitOfWork.Persons.FindPagedAsync(1, 1000);
        if (existing.Items.Any(p => p.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase)))
        {
            result.Errors.Add(new ImportErrorReportDto($"{dto.FirstName} {dto.LastName}", new List<string> { "Contact with this email already exists." }));
            return;
        }

        var person = dto.ToEntity();
        person.Id = Guid.NewGuid();
        person.CreatedAt = DateTime.UtcNow;
        person.Notes = new List<Note>
        {
            new Note { Id = Guid.NewGuid(), Content = "Imported", CreatedAt = DateTime.UtcNow, CreatedBy = importedBy }
        };

        await _unitOfWork.Persons.AddAsync(person);
        result.Imported.Add(new ImportSummaryDto(person.GetDisplayName(), "Person", person.Id));
    }

    private async Task ProcessCompanyAsync(CreateCompanyDto dto, string importedBy, ImportResultDto result)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            result.Errors.Add(new ImportErrorReportDto(dto.Name ?? "Unknown Company", new List<string> { "Name is required" }));
            return;
        }

        var existing = await _unitOfWork.Companies.GetAllAsync();
        if (existing.Any(c => c.Name.Equals(dto.Name, StringComparison.OrdinalIgnoreCase)))
        {
            result.Errors.Add(new ImportErrorReportDto(dto.Name, new List<string> { "Company with this name already exists." }));
            return;
        }

        var company = new Company
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            NIP = dto.NIP,
            REGION = dto.REGON,
            Email = dto.Email ?? "",
            Phone = dto.Phone ?? "",
            CreatedAt = DateTime.UtcNow,
            Notes = new List<Note>
            {
                new Note { Id = Guid.NewGuid(), Content = "Imported", CreatedAt = DateTime.UtcNow, CreatedBy = importedBy }
            }
        };

        await _unitOfWork.Companies.AddAsync(company);
        result.Imported.Add(new ImportSummaryDto(company.GetDisplayName(), "Company", company.Id));
    }

    private async Task ProcessOrganizationAsync(CreateOrganizationDto dto, string importedBy, ImportResultDto result)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            result.Errors.Add(new ImportErrorReportDto(dto.Name ?? "Unknown Organization", new List<string> { "Name is required" }));
            return;
        }

        var existing = await _unitOfWork.Organizations.GetAllAsync();
        if (existing.Any(o => o.Name.Equals(dto.Name, StringComparison.OrdinalIgnoreCase)))
        {
            result.Errors.Add(new ImportErrorReportDto(dto.Name, new List<string> { "Organization with this name already exists." }));
            return;
        }

        var org = new Organization
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Email = dto.Email ?? "",
            Phone = dto.Phone ?? "",
            CreatedAt = DateTime.UtcNow,
            Notes = new List<Note>
            {
                new Note { Id = Guid.NewGuid(), Content = "Imported", CreatedAt = DateTime.UtcNow, CreatedBy = importedBy }
            }
        };

        await _unitOfWork.Organizations.AddAsync(org);
        result.Imported.Add(new ImportSummaryDto(org.GetDisplayName(), "Organization", org.Id));
    }
}
