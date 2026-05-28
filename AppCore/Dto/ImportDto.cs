using AppCore.Models;

namespace AppCore.Dto;

public record ImportResultDto
{
    public List<ImportSummaryDto> Imported { get; init; } = new();
    public List<ImportErrorReportDto> Errors { get; init; } = new();
}

public record ImportSummaryDto(string Name, string Type, Guid Id);

public record ImportErrorReportDto(string ContactData, List<string> Errors);

public record ContactImportDto
{
    public List<CreatePersonDto>? People { get; init; }
    public List<CreateCompanyDto>? Companies { get; init; }
    public List<CreateOrganizationDto>? Organizations { get; init; }
}

public record CreateCompanyDto(
    string Name,
    string? NIP,
    string? REGON,
    string? Email,
    string? Phone,
    AddressDto? Address
);

public record CreateOrganizationDto(
    string Name,
    string? Email,
    string? Phone,
    AddressDto? Address
);
