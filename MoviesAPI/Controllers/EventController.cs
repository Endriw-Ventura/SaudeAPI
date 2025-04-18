using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data;
using MoviesAPI.Data.DTOs.Event;
using MoviesAPI.Models;
using MoviesAPI.Services;
using System.Numerics;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("Event")]
[Authorize]
public class EventController : ControllerBase
{
    private EventService _eventService;

    public EventController(EventService eventService)
    {
        _eventService = eventService;
    }

    [HttpPost]
    public IActionResult AddEvent([FromBody] CreateEventDTO eventDTO)
    {
        Event? evento = _eventService.CreateEvent(eventDTO);
        if (evento == null)
            return NotFound();

        return CreatedAtAction(nameof(GetEventByID), new { id = evento.Id }, evento);
    }

    [HttpGet]
    public IEnumerable<Event> GetEvents([FromQuery] int skip = 0, [FromQuery] int take = 20)
    {
        return _eventService.GetEvents(skip, take);
    }

    [HttpGet("doctor/{id}")]
    public IEnumerable<Event> GetEventsFromDoctor(int id)
    {
        return _eventService.GetEventsFromDoctor(id);
    }

    [HttpGet("user/{id}")]
    public IEnumerable<Event> GetEventsFromUser(int id)
    {
        return _eventService.GetEventFromUser(id);
    }

    [HttpGet("{id}")]
    public IActionResult GetEventByID(int id)
    {
        Event? evento = _eventService.GetEventByID(id);
        if (evento == null)
            return NotFound();

        return Ok(evento);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateEvent(int id, [FromBody] UpdateEventDTO updatedEvent)
    {
        Event? evento = _eventService.GetEventByID(id);
        if (evento == null)
            return NotFound();

        _eventService.UpdateEvent(id, updatedEvent);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEvent(int id)
    {
        Event? evento = _eventService.GetEventByID(id);
        if (evento == null)
            return NotFound();

        _eventService.DeleteEvent(evento);   
        return NoContent();
    }
}
