using AppCore.Dto;
using AppCore.Models;

namespace AppCore.Interfaces;

public interface IPersonService
{
    Task<PagedResult<PersonDto>> FindAllPeoplePaged(int page, int size);
    Task<IAsyncEnumerable<PersonDto>> FindPeopleFromCompany(Guid companyId);
    Task<Person> CreatePerson(PersonDto personDto);
    Task<Person> UpdatePerson(PersonDto personDto);
    Task DeletePerson(Guid id);
    Task AddTag(Guid id, Tag tag);
    Task AddNote(Guid id, Note note);
}