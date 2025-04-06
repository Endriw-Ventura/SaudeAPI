using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("Specialty")]
public class SpecialtyController : ControllerBase
{
    private APIContext _context;

    public SpecialtyController(APIContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult AddSpecialty([FromBody] Specialty specialty)
    {
        _context.Specialties.Add(specialty);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetSpecialtyByID), new { id = specialty.Id }, specialty);
    }

    [HttpGet]
    public IEnumerable<Specialty> GetSpecialties([FromQuery] int skip = 0, [FromQuery] int take = 20)
    {
        return _context.Specialties.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult GetSpecialtyByID(int id)
    {
        Specialty? specialty = _context.Specialties.FirstOrDefault(m => m.Id == id);
        if (specialty == null)
            return NotFound();

        return Ok(specialty);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateSpecialty(int id, [FromBody] Specialty updatedSpecialty)
    {
        Specialty? specialty = _context.Specialties.FirstOrDefault(m => m.Id == id);
        if (specialty == null)
            return NotFound();

        specialty.Name = updatedSpecialty.Name;

        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteSpecialty(int id)
    {
        Specialty? specialty = _context.Specialties.FirstOrDefault(m => m.Id == id);
        if (specialty == null)
            return NotFound();

        _context.Specialties.Remove(specialty);
        _context.SaveChanges();
        return NoContent();
    }
}
