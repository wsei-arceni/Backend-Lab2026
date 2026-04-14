using AppCore.Dto;
using AppCore.Models;

namespace AppCore.Interfaces;

public interface IPersonService
{
    Task<PersonDto?> GetById(Guid id);
    //TODO: Similar methods? (Lab5)
    Task<PersonDto> GetPerson(Guid personId);
    Task<PagedResult<PersonDto>> FindAllPeoplePaged(int page, int size);
    Task<IAsyncEnumerable<PersonDto>> FindPeopleFromCompany(Guid companyId);
    Task<Person> CreatePerson(PersonDto personDto);
    Task<Person> UpdatePerson(PersonDto personDto);
    Task<Person> AddPerson(CreatePersonDto personDto);
    Task DeletePerson(Guid id);
    Task AddTag(Guid id, Tag tag);
    Task<Note> AddNote(Guid id, Note note);
    Task<Note> AddNoteToPerson(Guid personId, CreateNoteDto noteDto);
}