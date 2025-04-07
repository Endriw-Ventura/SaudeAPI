using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesAPI.Models;

public class User
{
    [Key]
    [Required]
    public int Id { get; internal set; }

    [ForeignKey("Address")]
    public int AddressId { get; set; }

    [ForeignKey("UserInfo")]
    public int UserInfoId { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string CPF { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public  List<Event> Events { get; set; }
    public Address Address { get; set; }
    public UserInfo UserInfo { get; set; }

}
