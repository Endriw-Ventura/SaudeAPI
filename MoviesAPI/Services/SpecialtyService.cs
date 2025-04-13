using MoviesAPI.Data.DTOs.User;
using MoviesAPI.Data;
using MoviesAPI.Models;
using MoviesAPI.Data.DTOs.Specialty;

namespace MoviesAPI.Services
{
    public class SpecialtyService
    {
        private readonly APIContext _context;

        public SpecialtyService(APIContext context)
        {
            _context = context;
        }

        public Specialty CreateSpecialty(CreateSpecialtyDTO specialtyDTO)
        {
            var specialty = new Specialty
            {
               Name = specialtyDTO.Name,
            };

            AddSpecialty(specialty);
            return specialty;
        }

        public void AddSpecialty(Specialty specialty)
        {
            _context.Specialties.Add(specialty);
            _context.SaveChanges();
        }

        public IEnumerable<Specialty> GetSpecialties(int skip, int take)
        {
            return _context.Specialties.Skip(skip).Take(take);
        }

        public Specialty? GetSpecialtyByID(int id)
        {
            return _context.Specialties.FirstOrDefault(m => m.Id == id);
        }

        public Specialty? UpdateSpecialty(int id, UpdateSpecialtyDTO updatedSpecialty)
        {
            Specialty? newSpecialty = GetSpecialtyByID(id);

            if (newSpecialty == null)
            {
                return null;
            }

            if (newSpecialty.Name != updatedSpecialty.Name)
            {
                newSpecialty.Name = updatedSpecialty.Name;
            }

            _context.Specialties.Update(newSpecialty);
            _context.SaveChanges();
            return newSpecialty;
        }

        public void DeleteSpecialty(Specialty specialty)
        {
            _context.Specialties.Remove(specialty);
            _context.SaveChanges();
        }
    }
}
