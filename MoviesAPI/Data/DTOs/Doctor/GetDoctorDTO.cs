using MoviesAPI.Data.DTOs.Event;
using MoviesAPI.Data.DTOs.Specialty;

namespace MoviesAPI.Data.DTOs.Doctor
{
    public class GetDoctorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public TimeOnly InitialHour { get; set; }
        public TimeOnly FinalHour { get; set; }
        public string WeekDays { get; set; }
        public GetSpecialtyDTO Specialty { get; set; }
        public List<GetEventDTO> Events { get; set; }
    }
}
