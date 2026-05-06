using AppCore.Authorization;
using AppCore.Dto;
using AppCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controller;

[ApiController]
[Route("/api/contacts")]
public class ContactsController(IPersonService service): ControllerBase
{
    
    [HttpGet("")]
    [Authorize(Policy = nameof(CrmPolicies.ReadOnlyAccess))]
    public async Task<IActionResult> GetAllPersons([FromQuery] int page = 0, [FromQuery] int size = 10)
    {
        return Ok(await service.FindAllPeoplePaged(page, size));
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPerson([FromRoute] Guid id)
    {
        var dto = await service.GetById(id);
        if (dto == null) return NotFound();
        return Ok(dto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePerson([FromRoute] Guid id, [FromBody] UpdatePersonDto dto)
    {
        return Ok(await service.UpdatePerson(id, dto));
    }
    
    [HttpGet("{id:guid}/notes")]
    public async Task<IActionResult> GetNotes([FromRoute] Guid id)
    {
        var person = await service.GetById(id);
        if (person == null) return NotFound();
        return Ok(person.Notes);
    }

    [HttpPost("{id:guid}/notes")]
    public async Task<IActionResult> AddNote([FromRoute] Guid id, [FromBody] CreateNoteDto dto)
    {
        return Ok(await service.AddNote(id, dto));
    }
    
    [HttpDelete("{userId:guid}/notes/{noteId:guid}")]
    public async Task<IActionResult> DeleteNote([FromRoute] Guid userId, [FromRoute] Guid noteId)
    {
        await service.DeleteNote(userId, noteId);
        return NoContent();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonDto dto)
    {
        var result = await service.AddPerson(dto);
        return CreatedAtAction(nameof(GetPerson), new { id = result.Id }, result);
    }
}