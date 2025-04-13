using MoviesAPI.Data.DTOs.User;
using MoviesAPI.Data;
using MoviesAPI.Models;
using MoviesAPI.Data.DTOs.Doctor;

namespace MoviesAPI.Services
{
    public class DoctorService
    {
        private readonly APIContext _context;
        private readonly SpecialtyService _specialtyService;

        public DoctorService(APIContext context, SpecialtyService specialtyService)
        {
            _context = context;
            _specialtyService = specialtyService;
        }

        public Doctor CreateDoctor(CreateDoctorDTO doctorDTO)
        {
            var doctor = new Doctor
            {
                Name = doctorDTO.Name,
                Surname = doctorDTO.Surname,
                WeekDays = doctorDTO.WeekDays,
                CRM = doctorDTO.CRM,
                InitialHour = doctorDTO.InitialHour,
                FinalHour = doctorDTO.FinalHour,
                Password = doctorDTO.Password,
                Specialty = _specialtyService.GetSpecialtyByID(doctorDTO.IdSpecialty),
                Price = doctorDTO.Price
            };

            AddDoctor(doctor);
            return doctor;
        }

        public void AddDoctor(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
        }

        public IEnumerable<Doctor> GetDoctors(int skip, int take)
        {
            return _context.Doctors.Skip(skip).Take(take);
        }

        public Doctor? GetDoctorByID(int id)
        {
            return _context.Doctors.FirstOrDefault(m => m.Id == id);
        }

        public Doctor? UpdateDoctor(int id, UpdateDoctorDTO updatedDoctor)
        {
            Doctor? newDoctor = GetDoctorByID(id);

            if (newDoctor == null)
            {
                return null;
            }

            if (newDoctor.Price != updatedDoctor.Price)
            {
                newDoctor.Price = updatedDoctor.Price;
            }

            if (newDoctor.Name != updatedDoctor.Name)
            {
                newDoctor.Name = updatedDoctor.Name;
            }

            if (newDoctor.Surname != updatedDoctor.Surname)
            {
                newDoctor.Surname = updatedDoctor.Surname;
            }

            if (newDoctor.InitialHour != updatedDoctor.InitialHour)
            {
                newDoctor.InitialHour = updatedDoctor.InitialHour;
            }

            if (newDoctor.FinalHour != updatedDoctor.FinalHour)
            {
                newDoctor.FinalHour = updatedDoctor.FinalHour;
            }

            if (newDoctor.WeekDays != updatedDoctor.WeekDays)
            {
                newDoctor.WeekDays = updatedDoctor.WeekDays;
            }

            if (newDoctor.Specialty.Id != updatedDoctor.IdSpecialty)
            {
                Specialty? specialty = _specialtyService.GetSpecialtyByID(updatedDoctor.IdSpecialty);

                if (specialty != null)
                {
                    newDoctor.Specialty = specialty;
                }
            }

            _context.Doctors.Update(newDoctor);
            _context.SaveChanges();
            return newDoctor;
        }

        public void DeleteDoctor(Doctor doctor)
        {
            _context.Doctors.Remove(doctor);
            _context.SaveChanges();
        }
    }
}
