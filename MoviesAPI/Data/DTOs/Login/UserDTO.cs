namespace MoviesAPI.Data.DTOs.Login
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
