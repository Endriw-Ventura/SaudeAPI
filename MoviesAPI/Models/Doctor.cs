using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesAPI.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        [ForeignKey("Weekdays")]
        public int WeekdaysId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CRM { get; set; }
        public string Price { get; set; }
        public string Password { get; set; }
        public TimeOnly InitialHour { get; set; }
        public TimeOnly FinalHour { get; set; }
        public Weekdays WeekDays { get; set; }
        public Specialty Specialty { get; set; }
        public List<Event> Events { get; set; }
    }
}
