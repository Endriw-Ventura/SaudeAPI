using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MoviesAPI.Data;
using MoviesAPI.Data.DTOs;
using MoviesAPI.Data.DTOs.User;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("User")]
public class UserController : ControllerBase
{
    private APIContext _context;

    public UserController(APIContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult AddUser([FromBody] CreateUserDTO user)
    {
        User newUser = new User { 
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            CPF = user.CPF,
            Password = user.Password,
            Address = new Address {
                Country = user.Address.Country,
                State = user.Address.State,
                City = user.Address.City,
                Neighborhood = user.Address.Neighborhood,
                Number = user.Address.Number,
                StreetName = user.Address.StreetName,
                ZipCode = user.Address.ZipCode,
                Complement = user.Address.Complement,
            },
            UserInfo = new UserInfo
            {
                Allergies = user.UserInfo.Allergies,
                Cirurgies = user.UserInfo.Cirurgies,
                BloodType = user.UserInfo.BloodType,
                MedicalCondition = user.UserInfo.MedicalCondition,
                MotherName = user.UserInfo.MotherName,
                Medications = user.UserInfo.Medications,
                PreviousCirurgies = user.UserInfo.PreviousCirurgies,
            },
        };
        _context.Users.Add(newUser);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetUserByID), new { id = newUser.Id }, newUser);
    }

    [HttpGet]
    public IEnumerable<User> GetUsers([FromQuery] int skip = 0, [FromQuery] int take = 20)
    {
        return _context.Users.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult GetUserByID(int id)
    {
        User? user = _context.Users.FirstOrDefault(m => m.Id == id);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
    {
        User? user = _context.Users.FirstOrDefault(m => m.Id == id);
        if (user == null)
            return NotFound();
        
        user.Name = updatedUser.Name;

        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        User? user = _context.Users.FirstOrDefault(m => m.Id == id);
        if (user == null)
            return NotFound();

        _context.Users.Remove(user);
        _context.SaveChanges();
        return NoContent();
    }
}
