using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data.DTOs.Doctor;
using MoviesAPI.Models;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("Doctor")]
[Authorize]
public class DoctorController : ControllerBase
{
    private readonly DoctorService _doctorService;

    public DoctorController(DoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult AddDoctor([FromBody] CreateDoctorDTO doctorDTO)
    {
        Doctor doctor = _doctorService.CreateDoctor(doctorDTO);
        return CreatedAtAction(nameof(GetDoctorByID), new { id = doctor.Id }, doctor);
    }

    [HttpGet]
    public IEnumerable<Doctor> GetDoctors([FromQuery] int skip = 0, [FromQuery] int take = 20)
    {
        return _doctorService.GetDoctors(skip, take);
    }

    [HttpGet("{id}")]
    public IActionResult GetDoctorByID(int id)
    {
        Doctor? doctor = _doctorService.GetDoctorByID(id);
        if (doctor == null)
            return NotFound();

        return Ok(doctor);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateDoctor(int id, [FromBody] UpdateDoctorDTO updatedDoctor)
    {

        Doctor? doctor = _doctorService.UpdateDoctor(id, updatedDoctor);
        if (doctor == null)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteDoctor(int id)
    {
        Doctor? doctor = _doctorService.GetDoctorByID(id);
        if (doctor == null)
            return NotFound();

        _doctorService.DeleteDoctor(doctor);
        return NoContent();
    }
}
