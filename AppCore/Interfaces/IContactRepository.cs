using AppCore.Dto;
using AppCore.Models;

namespace AppCore.Interfaces;

public interface IContactRepository: IGenericRepositoryAsync<Contact>
{
    public PagedResult<Contact> FindContactByDto(ContactSearchDto dto);
    public Contact FindByTag(Tag tag);
    public void AddNote(Guid id, Note note);
    public List<Note> GetNotes(Guid id);
    public void AddTag(Guid id, Tag tag);
    public void RemoveTag(Guid id, Tag tag);
}