using MoviesAPI.Data.DTOs.Address;
using MoviesAPI.Data.DTOs.UserInfo;

namespace MoviesAPI.Data.DTOs.User
{
    public class GetUserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public string CPF { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public GetAddressDTO Address { get; set; }
        public GetUserInfoDTO UserInfo { get; set; }
    }
}
