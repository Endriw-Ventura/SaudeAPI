using MoviesAPI.Data.DTOs.Doctor;
using MoviesAPI.Data.DTOs.User;

namespace MoviesAPI.Data.DTOs.Event
{
    public class GetEventDTO
    {
        public GetUserDTO Pacient { get; set; }
        public GetDoctorDTO Doctor { get; set; }
        public DateTime Moment { get; set; }
    }
}
