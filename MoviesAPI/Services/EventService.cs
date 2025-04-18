using MoviesAPI.Data;
using MoviesAPI.Models;
using MoviesAPI.Data.DTOs.Event;

namespace MoviesAPI.Services
{
    public class EventService
    {
        private readonly APIContext _context;
        private readonly UserService _userService;
        private readonly DoctorService _doctorService;

        public EventService(APIContext context, UserService userService, DoctorService doctorService)
        {
            _context = context;
            _userService = userService;
            _doctorService = doctorService;
        }

        public Event? CreateEvent(CreateEventDTO eventDTO)
        {
            var pacient = _userService.GetUserByID(eventDTO.IdPacient);
            
            if (pacient is null)
                return null;
            
            var doctor = _doctorService.GetDoctorByID(eventDTO.IdDoctor);
            if (doctor is null)
                return null;

            var evento = new Event
            {
                Pacient = pacient,
                Doctor = doctor,
                Moment = eventDTO.Moment
            };

            AddEvent(evento);
            return evento;
        }

        public void AddEvent(Event evento)
        {
            _context.Events.Add(evento);
            _context.SaveChanges();
        }

        public IEnumerable<Event> GetEvents(int skip, int take)
        {
            return _context.Events.Skip(skip).Take(take);
        }

        public Event? GetEventByID(int id)
        {
            return _context.Events.FirstOrDefault(m => m.Id == id);
        }

        public Event? UpdateEvent(int id, UpdateEventDTO updatedDoctor)
        {
            return null;
        }

        public void DeleteEvent(Event evento)
        {
            _context.Events.Remove(evento);
            _context.SaveChanges();
        }

        public IEnumerable<Event> GetEventsFromDoctor(int id)
        {
            return _context.Events.Where(e => e.Doctor.Id == id);
        }

        public IEnumerable<Event> GetEventFromUser(int id)
        {
            return _context.Events.Where(e => e.Pacient.Id == id);
        }
    }
}
