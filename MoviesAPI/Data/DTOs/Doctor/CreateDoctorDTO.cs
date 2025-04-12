using MoviesAPI.Data.DTOs.Event;
using MoviesAPI.Data.DTOs.Specialty;

namespace MoviesAPI.Data.DTOs.Doctor
{
    public class CreateDoctorDTO
    {
        public string Name { get; set; }
        public string CRM { get; set; }
        public string Price { get; set; }
        public string Password { get; set; }
        public TimeOnly InitialHour { get; set; }
        public TimeOnly FinalHour { get; set; }
        public string WeekDays { get; set; }
        public CreateSpecialtyDTO Specialty { get; set; }
    }
}
