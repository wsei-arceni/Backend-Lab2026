using AppCore.Models;

namespace AppCore.Dto;

public record NoteDto
{
    public Guid Id { get; init; }
    public string Content { get; init; }
    public DateTime CreatedAt { get; init; }
    public string CreatedBy { get; init; }

    public static NoteDto FromNote(Note note) => new NoteDto
    {
        Id = note.Id,
        Content = note.Content,
        CreatedAt = note.CreatedAt,
        CreatedBy = note.CreatedBy,
    };
}

public record CreateNoteDto(
    string Content,
    string CreatedBy
)
{
    public Note ToEntity()
    {
        return new Note
        {
            Content = Content,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = CreatedBy
        };
    }
}

public record UpdateNoteDto(
    string Content
);