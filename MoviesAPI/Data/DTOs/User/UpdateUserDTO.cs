using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.DTOs.User;

public class UpdateUserDTO
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
}
