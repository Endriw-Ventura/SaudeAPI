using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data;
using MoviesAPI.Data.DTOs.Exam;
using MoviesAPI.Models;

namespace MoviesAPI.Services
{
    public class ExamService
    {
        private readonly APIContext _context;
        private readonly UserService _userService;
        private readonly DoctorService _doctorService;

        public ExamService(APIContext context, UserService userService, DoctorService doctorService)
        {
            _context = context;
            _userService = userService;
            _doctorService = doctorService;
        }

        public Exam? CreateExam(CreateExamDTO examDTO)
        {
            var pacient = _userService.GetUserByID(examDTO.IdPacient);

            if (pacient is null)
                return null;

            var exam = new Exam
            {
                Pacient = pacient,
                ExamName = examDTO.ExamName,
                Moment = examDTO.Moment
            };

            AddExam(exam);
            return exam;
        }

        public void AddExam(Exam exam)
        {
            _context.Exams.Add(exam);
            _context.SaveChanges();
        }

        public IEnumerable<Exam> GetExams(int skip, int take)
        {
            return _context.Exams.Skip(skip).Take(take);
        }

        public Exam? GetExamByID(int id)
        {
            return _context.Exams.FirstOrDefault(m => m.Id == id);
        }

        public Exam? UpdateExam(int id, UpdateExamDTO updatedDoctor)
        {
            return null;
        }

        public void DeleteExam(Exam exam)
        {
            _context.Exams.Remove(exam);
            _context.SaveChanges();
        }

        public IEnumerable<Exam> GetExamsFromUser(int id)
        {
            return _context.Exams.Where(e => e.Pacient.Id == id).Include(d => d.Pacient);
        }

        public IEnumerable<Exam> GetExamsFromDoctor(int id)
        {
            return _context.Exams
                        .Where(exam => _context.Events
                            .Where(ev => ev.Doctor.Id == id)
                            .Select(ev => ev.Pacient.Id)
                            .Distinct()
                            .Contains(exam.Pacient.Id)
                        )
                        .Include(exam => exam.Pacient)
                        .ToList();
        }

        public IEnumerable<User> GetAvailableUsersForExam(int doctorId)
        {
            return _context.Events.Where(u => u.Doctor.Id == doctorId).Select(e => e.Pacient).Distinct().ToList();
        }
    }
}
