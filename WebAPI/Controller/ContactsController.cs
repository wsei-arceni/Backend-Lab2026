using AppCore.Dto;
using AppCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controller;

[ApiController]
[Route("/api/contacts")]
public class ContactsController(IPersonService service): ControllerBase
{

    public async Task<IActionResult> GetAllPersons(int page, int size)
    {
        return Ok(await service.FindAllPeoplePaged(page, size));
    }
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPerson(Guid id)
    {
        var dto = await service.GetById(id);
        if (dto == null) return NotFound();
        return Ok(dto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePerson(Guid id, [FromBody] PersonDto dto)
    {
        var p = await service.GetById(id);
        if (p == null) return NotFound();
        return Ok(await service.UpdatePerson(p));
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreatePersonDto dto)
    {
        var result = await service.AddPerson(dto);
        return CreatedAtAction(nameof(GetPerson), new { id = result.Id }, result);
    }
}