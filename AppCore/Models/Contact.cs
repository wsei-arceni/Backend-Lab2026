using AppCore.ValueObjects;

namespace AppCore.Models;

public abstract class Contact: EntityBase
{
    public string Email { get; set; }
    public string Phone { get; set; }
    public Address Address { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatetAt { get; set; }
    public ContactStatus Status { get; set; }
    public List<Tag> Tags { get; set; }
    public List<Note>? Notes { get; set; }
}

public class Note: EntityBase
{
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
}

public class Tag: EntityBase
{
    public string Name { get; set; }
    public string Color { get; set; }
}