using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data.DTOs.User;
using MoviesAPI.Models;
using MoviesAPI.Services;
using System.Security.Claims;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("User")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult AddUser([FromBody] CreateUserDTO userDTO)
    {
        User user = _userService.CreateUser(userDTO);
        return CreatedAtAction(nameof(GetUserByID), new { id = user.Id }, user);
    }

    [HttpGet]
    public IEnumerable<User> GetUsers([FromQuery] int skip = 0, [FromQuery] int take = 20)
    {
        return _userService.GetUsers(skip, take);
    }

    [HttpGet("{id}")]
    public IActionResult GetUserByID(int id)
    {
        User? user = _userService.GetUserByID(id);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] UpdateUserDTO updatedUser)
    {

        User? user = _userService.UpdateUser(id, updatedUser);
        if (user == null)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        User? user = _userService.GetUserByID(id);
        if (user == null)
            return NotFound();

        _userService.DeleteUser(user);
        return NoContent();
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult GetCurrentUser()
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var role = User.FindFirst(ClaimTypes.Role)?.Value;
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (email == null || role == null)
            return Unauthorized();

        var user = new
        {
            Id = id,
            Email = email,
            Role = role
        };

        return Ok(user);
    }
}
