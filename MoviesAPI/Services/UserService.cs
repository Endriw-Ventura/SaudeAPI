using MoviesAPI.Data.DTOs.User;
using MoviesAPI.Data;
using MoviesAPI.Models;

namespace MoviesAPI.Services
{
    public class UserService
    {
        private readonly APIContext _context;
        private readonly AddressService _addressService;
        private readonly UserInfoService _userInfoService;

        public UserService(APIContext context, AddressService addressService, UserInfoService userInfoService)
        {
            _context = context;
            _addressService = addressService;
            _userInfoService = userInfoService;
        }

        public User CreateUser(CreateUserDTO userDto)
        {
            var user = new User
            {
                Name = userDto.Name,
                Surname = userDto.Surname,
                Email = userDto.Email,
                CPF = userDto.CPF,
                Password = userDto.Password,
                Address = _addressService.CreateAddress(userDto.Address),
                UserInfo = _userInfoService.CreateUserInfo(userDto.UserInfo)
            };

            AddUser(user);
            return user;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetUsers(int skip, int take)
        {
            return _context.Users.Skip(skip).Take(take);
        }

        public User? GetUserByID(int id)
        {
            return _context.Users.FirstOrDefault(m => m.Id == id);
        }

        public User? UpdateUser(int id, UpdateUserDTO updatedUser)
        {
            User? newUser = GetUserByID(id);

            if (newUser == null) {
                return null;
            }

            if (newUser.Email != updatedUser.Email) {
                newUser.Email = updatedUser.Email;
            }

            if (newUser.Name != updatedUser.Name) {
                newUser.Name = updatedUser.Name;
            }

            if (newUser.Surname != updatedUser.Surname) {
                newUser.Surname = updatedUser.Surname;
            }

            _context.Users.Update(newUser);
            _context.SaveChanges();
            return newUser;
        }

        public void DeleteUser(User user) {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
