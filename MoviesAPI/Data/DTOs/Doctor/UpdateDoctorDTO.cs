using MoviesAPI.Data.DTOs.Event;
using MoviesAPI.Data.DTOs.Specialty;

namespace MoviesAPI.Data.DTOs.Doctor
{
    public class UpdateDoctorDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Price { get; set; }
        public TimeOnly InitialHour { get; set; }
        public TimeOnly FinalHour { get; set; }
        public string WeekDays { get; set; }
        public int IdSpecialty { get; set; }
    }
}
