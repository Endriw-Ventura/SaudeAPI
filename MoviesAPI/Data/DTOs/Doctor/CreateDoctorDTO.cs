using MoviesAPI.Data.DTOs.Event;
using MoviesAPI.Data.DTOs.Specialty;

namespace MoviesAPI.Data.DTOs.Doctor
{
    public class CreateDoctorDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string CRM { get; set; }
        public string Price { get; set; }
        public string Password { get; set; }
        public string InitialHour { get; set; }
        public string FinalHour { get; set; }
        public CreateWeekdaysDTO WeekdaysDTO { get; set; }
        public int SpecialtyId { get; set; }
    }
}
