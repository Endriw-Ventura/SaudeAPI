using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models;

public class Movie
{
    [Key]
    [Required]
    public int Id { get; internal set; }

    [Required(ErrorMessage = "The Title field is required")]
    [MaxLength(50, ErrorMessage = "The Title field must be at most 50 characters")]
    public string Title { get; set; }

    [Required(ErrorMessage = "The Gender field is required")]
    [MaxLength(50, ErrorMessage = "The Gender field must be at most 50 characters")]
    public string Gender { get; set; }

    [Required(ErrorMessage = "The Duration field is required")]
    [Range(45, 400, ErrorMessage = "The Duration field must be between 45 and 400 minutes")]
    public int Duration { get; set; }

}
