using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data;
using MoviesAPI.Data.DTOs.Specialty;
using MoviesAPI.Models;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("Specialty")]
[Authorize]
public class SpecialtyController : ControllerBase
{
    private readonly SpecialtyService _specialtyService;

    public SpecialtyController(SpecialtyService specialtyService)
    {
        _specialtyService = specialtyService;
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult AddSpecialty([FromBody] CreateSpecialtyDTO specialtyDTO)
    {
        Specialty specialty = _specialtyService.CreateSpecialty(specialtyDTO);
        return CreatedAtAction(nameof(GetSpecialtyByID), new { id = specialty.Id }, specialty);
    }

    [HttpGet]
    [AllowAnonymous]
    public IEnumerable<Specialty> GetSpecialties([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        return _specialtyService.GetSpecialties(skip, take);
    }

    [HttpGet("{id}")]
    public IActionResult GetSpecialtyByID(int id)
    {
        Specialty? specialty = _specialtyService.GetSpecialtyByID(id);
        if (specialty == null)
            return NotFound();

        return Ok(specialty);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateSpecialty(int id, [FromBody] UpdateSpecialtyDTO updatedSpecialty)
    {
        Specialty? specialty = _specialtyService.UpdateSpecialty(id, updatedSpecialty);
        if (specialty == null)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteSpecialty(int id)
    {
        Specialty? specialty = _specialtyService.GetSpecialtyByID(id);
        if (specialty == null)
            return NotFound();

        _specialtyService.DeleteSpecialty(specialty);
        return NoContent();
    }
}
