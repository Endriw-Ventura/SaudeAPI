namespace MoviesAPI.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string StreetName { get; set; }
        public string Complement { get; set; }
        public string Number { get; set; }
        public string ZipCode { get; set; }
        public User User { get; set; }
    }
}
