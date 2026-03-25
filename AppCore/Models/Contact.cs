using AppCore.ValueObjects;

namespace AppCore.Models;

public abstract class Contact
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public Address Address { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatetAt { get; set; }
    public ContactStatus Status { get; set; }
    public List<Tag> Tags { get; set; }
    public List<Note> Notes { get; set; }

    public string GetDisplayName()
    {
        /* TODO: Class don't have Name */
        throw new NotImplementedException();
    }
}

public class Note
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
}

public class Tag
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
}