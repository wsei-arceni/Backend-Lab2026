using AppCore.Dto;
using AppCore.Interfaces;
using AppCore.Models;

namespace Infrastructure.Memory;

public class MemoryPersonService(IContactUnitOfWork unitOfWork) : IPersonService
{
    public async Task<PagedResult<PersonDto>> FindAllPeoplePaged(int page, int size)
    {
        var poeple = await unitOfWork.Persons.FindPagedAsync(page, size);
        var items=  poeple.Items.Select(p => new PersonDto()
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName, 
                Email = p.Email,
                Phone = p.Phone,
                Status = p.Status
            }
                
        );
        return new PagedResult<PersonDto>(, poeple.TotalCount, poeple.Page, poeple.PageSize);
    }

    public async Task<IAsyncEnumerable<PersonDto>> FindPeopleFromCompany(Guid companyId)
    {
        // implementacja
    }

    public Task<Person> CreatePerson(PersonDto personDto)
    {
        throw new NotImplementedException();
    }

    public Task<Person> UpdatePerson(PersonDto personDto)
    {
        /* TODO: What UpdatePerson have to do (Lab 3,2) */
        throw new NotImplementedException();
    }

    public Task DeletePerson(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task AddTag(Guid id, Tag tag)
    {
        throw new NotImplementedException();
    }

    public Task AddNote(Guid id, Note note)
    {
        throw new NotImplementedException();
    }
}