using MoviesAPI.Data;
using MoviesAPI.Data.DTOs.Address;
using MoviesAPI.Models;

namespace MoviesAPI.Services
{
    public class AddressService
    {
        public Address CreateAddress(CreateAddressDTO addressDTO)
        {
            Address address = new Address
            {
                Country = addressDTO.Country,
                State = addressDTO.State,
                City = addressDTO.City,
                Neighborhood = addressDTO.Neighborhood,
                Number = addressDTO.Number,
                StreetName = addressDTO.StreetName,
                ZipCode = addressDTO.ZipCode,
                Complement = addressDTO.Complement
            };
            return address;
        }
    }
}
