using MoviesAPI.Data.DTOs.Doctor;
using MoviesAPI.Data.DTOs.User;

namespace MoviesAPI.Data.DTOs.Exam
{
    public class GetExamDTO
    {
        public GetUserDTO Pacient { get; set; }
        public string examName {  get; set; }
        public DateTime Moment { get; set; }
    }
}
