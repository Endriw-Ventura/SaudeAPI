using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.DTOs;

public class CreateMovieDTO
{
    [Required(ErrorMessage = "The Title field is required")]
    [StringLength(50, ErrorMessage = "The Title field must be at most 50 characters")]
    public string Title { get; set; }

    [Required(ErrorMessage = "The Gender field is required")]
    [StringLength(50, ErrorMessage = "The Gender field must be at most 50 characters")]
    public string Gender { get; set; }

    [Required(ErrorMessage = "The Duration field is required")]
    [Range(45, 400, ErrorMessage = "The Duration field must be between 45 and 400 minutes")]
    public int Duration { get; set; }
}
