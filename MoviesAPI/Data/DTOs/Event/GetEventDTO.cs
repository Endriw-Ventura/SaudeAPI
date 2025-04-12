namespace MoviesAPI.Data.DTOs.Event
{
    public class GetEventDTO
    {
        public int IdPacient { get; set; }
        public int IdDoctor { get; set; }
        public DateTime Moment { get; set; }
    }
}
