using MoviesAPI.Data.DTOs.User;
using MoviesAPI.Data;
using MoviesAPI.Models;
using MoviesAPI.Data.DTOs.Doctor;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
            var weekdays = CreateWeekdays(doctorDTO.WeekdaysDTO);

            _context.Weekdays.Add(weekdays);
            _context.SaveChanges();

            var doctor = new Doctor
            {
                Name = doctorDTO.Name,
                Surname = doctorDTO.Surname,
                Email = doctorDTO.Email, 
                CRM = doctorDTO.CRM,
                InitialHour = TimeOnly.Parse(doctorDTO.InitialHour),
                FinalHour = TimeOnly.Parse(doctorDTO.FinalHour),
                Password = doctorDTO.Password,
                SpecialtyId = doctorDTO.SpecialtyId,
                Price = doctorDTO.Price,
                WeekdaysId = weekdays.Id,
                WeekDays = weekdays
            };

            AddDoctor(doctor);
            return doctor;
        }

        public Weekdays CreateWeekdays(CreateWeekdaysDTO weekdaysDTO)
        {
            var weekdays = new Weekdays
            {
                Saturday = weekdaysDTO.Saturday,
                Monday = weekdaysDTO.Monday,
                Sunday = weekdaysDTO.Sunday,
                Thursday = weekdaysDTO.Thursday,
                Tuesday = weekdaysDTO.Tuesday,
                Friday = weekdaysDTO.Friday,
                Wednesday = weekdaysDTO.Wednesday
            };
            return weekdays;
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

        public IEnumerable<Doctor> GetAvailableDoctors(int idSpecialty, DateTime moment)
        {
            var weekday = moment.DayOfWeek;
            var time = moment.TimeOfDay;
            var convertedTime = TimeOnly.FromTimeSpan(time);
            var day = moment.Day;
            var month = moment.Month;
            var year = moment.Year;
            var predicado = DiaDaSemanaPredicate(weekday);

            return _context.Doctors
                    .Include(d => d.Events)
                    .Where(d => d.SpecialtyId == idSpecialty)
                    .Where(d => d.InitialHour <= convertedTime && d.FinalHour >= convertedTime)
                    .Where(predicado)
                    .Where(d => !d.Events
                    .Any(a => a.Moment.Date == moment.Date 
                    &&  a.Moment.TimeOfDay == moment.TimeOfDay)
                    );
        }

        private Expression<Func<Doctor, bool>> DiaDaSemanaPredicate(DayOfWeek dia)
        {
            return dia switch
            {
                DayOfWeek.Sunday => d => d.WeekDays.Sunday,
                DayOfWeek.Monday => d => d.WeekDays.Monday,
                DayOfWeek.Tuesday => d => d.WeekDays.Tuesday,
                DayOfWeek.Wednesday => d => d.WeekDays.Wednesday,
                DayOfWeek.Thursday => d => d.WeekDays.Thursday,
                DayOfWeek.Friday => d => d.WeekDays.Friday,
                DayOfWeek.Saturday => d => d.WeekDays.Saturday,
                _ => d => false
            };
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
