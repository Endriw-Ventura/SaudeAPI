namespace MoviesAPI.Models
{
    public class Event
    {
        public int Id { get; set; }
        public User Pacient { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime Moment { get; set; }
    }
}
