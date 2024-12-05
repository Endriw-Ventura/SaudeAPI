using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("Movies")]
public class MovieController : ControllerBase
{
    private static List<Movie> movies = new List<Movie>();
    private static int id = 0;

    [HttpPost]
    public IActionResult AddMovie([FromBody] Movie movie)
    {
        movie.Id = id++;
        movies.Add(movie);
        return CreatedAtAction(nameof(GetMovieByID), new { id = movie.Id }, movie);
    }

    [HttpGet]
    public IEnumerable<Movie> GetMovies([FromQuery] int skip = 0, [FromQuery] int take = 20)
    {
        return movies.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult GetMovieByID(int id)
    {
        Movie? movie = movies.FirstOrDefault(m => m.Id == id);
        if (movie == null)
            return NotFound();

        return Ok(movie);
    }


}
