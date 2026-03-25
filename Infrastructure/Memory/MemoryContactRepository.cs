using AppCore.Dto;
using AppCore.Interfaces;
using AppCore.Models;

namespace Infrastructure.Memory;

public class MemoryContactRepository: MemoryGenericRepository<Contact>, IContactRepository
{
    public PagedResult<Contact> FindContactByDto(ContactSearchDto dto)
    {
        throw new NotImplementedException();
    }

    public Contact FindByTag(Tag tag)
    {
        throw new NotImplementedException();
    }

    public void AddNote(Guid id, Note note)
    {
        throw new NotImplementedException();
    }

    public List<Note> GetNotes(Guid id)
    {
        throw new NotImplementedException();
    }

    public void AddTag(Guid id, Tag tag)
    {
        throw new NotImplementedException();
    }

    public void RemoveTag(Guid id, Tag tag)
    {
        throw new NotImplementedException();
    }
}