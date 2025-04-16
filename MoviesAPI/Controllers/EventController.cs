using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("Event")]
[Authorize]
public class EventController : ControllerBase
{
    private APIContext _context;

    public EventController(APIContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult AddEvent([FromBody] Event evento)
    {
        _context.Events.Add(evento);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetEventByID), new { id = evento.Id }, evento);
    }

    [HttpGet]
    public IEnumerable<Event> GetEvents([FromQuery] int skip = 0, [FromQuery] int take = 20)
    {
        return _context.Events.Skip(skip).Take(take);
    }

    [HttpGet]
    public IEnumerable<Event> GetEventsFromDoctor(int id, [FromQuery] int skip = 0, [FromQuery] int take = 20)
    {
        return _context.Events.Where(a => a.Doctor.Id == id).Skip(skip).Take(take);
    }

    [HttpGet]
    public IEnumerable<Event> GetEventsFromUser(int id, [FromQuery] int skip = 0, [FromQuery] int take = 20)
    {
        return _context.Events.Where(a => a.Pacient.Id == id).Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult GetEventByID(int id)
    {
        Event? evento = _context.Events.FirstOrDefault(m => m.Id == id);
        if (evento == null)
            return NotFound();

        return Ok(evento);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateEvent(int id, [FromBody] Event updatedEvent)
    {
        Event? evento = _context.Events.FirstOrDefault(m => m.Id == id);
        if (evento == null)
            return NotFound();

        evento.Moment = updatedEvent.Moment;

        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEvent(int id)
    {
        Event? evento = _context.Events.FirstOrDefault(m => m.Id == id);
        if (evento == null)
            return NotFound();

        _context.Events.Remove(evento);
        _context.SaveChanges();
        return NoContent();
    }
}
