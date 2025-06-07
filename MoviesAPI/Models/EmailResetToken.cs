namespace MoviesAPI.Models
{
    public class EmailResetToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime expire {  get; set; }
    }
}
