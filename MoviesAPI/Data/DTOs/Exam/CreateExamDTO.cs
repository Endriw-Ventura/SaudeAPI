namespace MoviesAPI.Data.DTOs.Exam
{
    public class CreateExamDTO
    {
        public int IdPacient { get; set; }
        public int IdDoctor { get; set; }
        public DateTime Moment { get; set; }
    }
}
