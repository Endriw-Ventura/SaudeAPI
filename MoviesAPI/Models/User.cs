using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models;

public class User
{
    [Key]
    [Required]
    public int Id { get; internal set; }

    //[Required(ErrorMessage = "The Name field is required")]
    //[StringLength(50, ErrorMessage = "The Name field must be at most 15 characters")]
    public string Name { get; set; }

    public string Surname { get; set; }

    public string CPF { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public  List<Event> Events { get; set; }

    public Address Address { get; set; }

}
