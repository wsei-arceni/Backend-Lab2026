using AppCore.Dto;
using AppCore.Interfaces;
using AppCore.Models;

namespace Infrastructure.Memory;

public class MemoryPersonService(IContactUnitOfWork unitOfWork) : IPersonService
{
    public async Task<PersonDto> GetById(Guid id)
    {
        var entity = await unitOfWork.Persons.GetByIdAsync(id);
        return PersonDto.FromPerson(entity);
    }

    public async Task<Person> UpdatePerson(UpdatePersonDto personDto)
    {
        var current = await unitOfWork.Persons.GetByIdAsync(personDto.Id);
        personDto.UpdateEntity(current);
        
        var updated = await unitOfWork.Persons.UpdateAsync(current);
        await unitOfWork.SaveChangesAsync();
        return updated;
    }

    public async Task<Person> AddPerson(CreatePersonDto personDto)
    {
        var entity = personDto.ToEntity();
        entity = await unitOfWork.Persons.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return await Task.FromResult(entity);
    }

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

    public Task DeletePerson(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task AddTag(Guid id, Tag tag)
    {
        throw new NotImplementedException();
    }

    public async Task<Note> AddNoteToPerson(Guid id, CreateNoteDto noteDto)
    {
        var p = await unitOfWork.Persons.GetByIdAsync(id);
        if (p == null) throw new KeyNotFoundException();
        if (p.Notes == null) p.Notes = new List<Note>();
        var note = noteDto.ToEntity();
        p.Notes.Add(note);
        return await Task.FromResult(note);
    }
}