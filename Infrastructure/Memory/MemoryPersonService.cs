using AppCore.Dto;
using AppCore.Interfaces;
using AppCore.Models;

namespace Infrastructure.Memory;

public class MemoryPersonService(IContactUnitOfWork unitOfWork) : IPersonService
{
    public async Task<PagedResult<PersonDto>> FindAllPeoplePaged(int page, int size)
    {
        var people = await unitOfWork.Persons.FindPagedAsync(page, size);
        var items=  people.Items.Select(p => new PersonDto()
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName, 
                Email = p.Email,
                Phone = p.Phone,
                Status = p.Status
            }
                
        );
        return new PagedResult<PersonDto>(items.ToList(), people.TotalCount, people.Page, people.PageSize);
    }

    public async Task<IAsyncEnumerable<PersonDto>> FindPeopleFromCompany(Guid companyId)
    {
        throw new NotImplementedException();
    }

    public Task<Person> CreatePerson(PersonDto personDto)
    {
        throw new NotImplementedException();
    }

    public Task<Person> UpdatePerson(PersonDto personDto)
    {
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