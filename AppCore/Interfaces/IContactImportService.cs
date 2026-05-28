using AppCore.Dto;

namespace AppCore.Interfaces;

public interface IContactImportService
{
    Task<ImportResultDto> ImportFromJsonDtoAsync(ContactImportDto dto, string importedBy);
    Task<ImportResultDto> ImportFromCsvAsync(Stream csvStream, string importedBy);
}
