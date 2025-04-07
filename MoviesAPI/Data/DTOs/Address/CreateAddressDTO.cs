namespace MoviesAPI.Data.DTOs.Address
{
    public class CreateAddressDTO
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string StreetName { get; set; }
        public string Complement { get; set; }
        public string Number { get; set; }
        public string ZipCode { get; set; }
    }
}
