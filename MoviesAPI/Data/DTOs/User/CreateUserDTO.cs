using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.DTOs.User;

public class CreateUserDTO
{
    public string Title { get; set; }
    public string Gender { get; set; }
    public int Duration { get; set; }
}
