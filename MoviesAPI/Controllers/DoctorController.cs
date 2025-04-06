using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MoviesAPI.Data;
using MoviesAPI.Data.DTOs;
using MoviesAPI.Models;
using System.Numerics;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("Doctor")]
public class DoctorController : ControllerBase
{
    private APIContext _context;

    public DoctorController(APIContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult AddDoctor([FromBody] Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetDoctorByID), new { id = doctor.Id }, doctor);
    }

    [HttpGet]
    public IEnumerable<Doctor> GetDoctors([FromQuery] int skip = 0, [FromQuery] int take = 20)
    {
        return _context.Doctors.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult GetDoctorByID(int id)
    {
        Doctor? doctor = _context.Doctors.FirstOrDefault(m => m.Id == id);
        if (doctor == null)
            return NotFound();

        return Ok(doctor);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateDoctor(int id, [FromBody] Doctor updatedDoctor)
    {
        Doctor? doctor = _context.Doctors.FirstOrDefault(m => m.Id == id);
        if (doctor == null)
            return NotFound();

        doctor.Name = updatedDoctor.Name;

        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteDoctor(int id)
    {
        Doctor? doctor = _context.Doctors.FirstOrDefault(m => m.Id == id);
        if (doctor == null)
            return NotFound();

        _context.Doctors.Remove(doctor);
        _context.SaveChanges();
        return NoContent();
    }
}
