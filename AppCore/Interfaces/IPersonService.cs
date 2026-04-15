using AppCore.Dto;
using AppCore.Models;

namespace AppCore.Interfaces;

public interface IPersonService
{
    Task<PersonDto> GetById(Guid id);
    Task<PagedResult<PersonDto>> FindAllPeoplePaged(int page, int size);
    Task<IAsyncEnumerable<PersonDto>> FindPeopleFromCompany(Guid companyId);
    Task<Person> UpdatePerson(UpdatePersonDto  personDto);
    Task<Person> AddPerson(CreatePersonDto personDto);
    Task DeletePerson(Guid id);
    Task AddTag(Guid id, Tag tag);
    Task<Note> AddNoteToPerson(Guid personId, CreateNoteDto noteDto);
}