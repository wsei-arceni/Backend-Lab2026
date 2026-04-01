using AppCore.Dto;
using AppCore.Interfaces;
using AppCore.Models;

namespace Infrastructure.Memory;

public class MemoryContactRepository: MemoryGenericRepository<Contact>, IContactRepository
{
    public PagedResult<Contact> FindContactByDto(ContactSearchDto dto)
    {
        var result = _data.Values.Where(ct => dto.Query.Contains(ct.Email));
        return new PagedResult<Contact>(result.ToList(), result.Count(), dto.Page, dto.PageSize);
    }

    public Contact FindByTag(Tag tag)
    {
        return _data.Values.FirstOrDefault(ct => ct.Tags.Contains(tag));;
    }

    public void AddNote(Guid id, Note note)
    {
        _data[id].Notes.Add(note);
    }

    public List<Note> GetNotes(Guid id)
    {
        return _data.Values.FirstOrDefault(ct => ct.Id == id).Notes;
    }

    public void AddTag(Guid id, Tag tag)
    {
        _data[id].Tags.Add(tag);
    }

    public void RemoveTag(Guid id, Tag tag)
    {
        _data[id].Tags.Remove(tag);
    }
}