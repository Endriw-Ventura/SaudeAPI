using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MoviesAPI.Data;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("Movies")]
public class MovieController : ControllerBase
{
    private MovieContext _context;

    public MovieController(MovieContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult AddMovie([FromBody] Movie movie)
    {
        _context.Movies.Add(movie);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetMovieByID), new { id = movie.Id }, movie);
    }

    [HttpGet]
    public IEnumerable<Movie> GetMovies([FromQuery] int skip = 0, [FromQuery] int take = 20)
    {
        return _context.Movies.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult GetMovieByID(int id)
    {
        Movie? movie = _context.Movies.FirstOrDefault(m => m.Id == id);
        if (movie == null)
            return NotFound();

        return Ok(movie);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateMovie(int id, [FromBody] Movie updatedMovie)
    {
        Movie? movie = _context.Movies.FirstOrDefault(m => m.Id == id);
        if (movie == null)
            return NotFound();

        movie = updatedMovie;
        _context.SaveChanges();

        return NoContent();
    }

    //[HttpPatch("{id}")]
    //public IActionResult UpdateMovie(int id, [FromBody] )
    //{
    //    Movie? movie = _context.Movies.FirstOrDefault(m => m.Id == id);
    //    if (movie == null)
    //        return NotFound();

    //    movie = updatedMovie;
    //    _context.SaveChanges();

    //    return NoContent();
    //}

    [HttpDelete("{id}")]
    public IActionResult DeleteMovie(int id)
    {
        Movie? movie = _context.Movies.FirstOrDefault(m => m.Id == id);
        if (movie == null)
            return NotFound();

        _context.Movies.Remove(movie);
        _context.SaveChanges();
        return NoContent();
    }
}
