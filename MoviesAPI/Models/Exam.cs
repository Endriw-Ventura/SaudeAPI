namespace MoviesAPI.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string ExamName { get; set; }
        public User Pacient { get; set; }
        public DateTime Moment { get; set; }
    }
}
