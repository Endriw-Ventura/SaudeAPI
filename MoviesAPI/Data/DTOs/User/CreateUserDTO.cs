using MoviesAPI.Data.DTOs.Address;
using MoviesAPI.Data.DTOs.UserInfo;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.DTOs.User;

public class CreateUserDTO
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public string CPF { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public CreateAddressDTO Address { get; set; }

    public CreateUserInfoDTO UserInfo { get; set; }
}
