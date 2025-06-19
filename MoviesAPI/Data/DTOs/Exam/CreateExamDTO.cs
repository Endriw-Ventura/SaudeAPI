namespace MoviesAPI.Data.DTOs.Exam
{
    public class CreateExamDTO
    {
        public int IdPacient { get; set; }
        public string ExamName { get; set; }
        public DateTime Moment { get; set; }
    }
}
