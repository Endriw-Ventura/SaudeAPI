namespace MoviesAPI.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Specialty Specialty { get; set; }
        public List<Event> Events { get; set; }
    }
}
