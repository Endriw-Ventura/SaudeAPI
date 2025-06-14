using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data.DTOs.Exam;
using MoviesAPI.Models;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("Exam")]
    [Authorize]
    public class ExamController : ControllerBase
    {
        private ExamService _examService;

        public ExamController(ExamService examService)
        {
            _examService = examService;
        }

        [HttpPost]
        public IActionResult AddEvent([FromBody] CreateExamDTO examDTO)
        {
            Exam? exam = _examService.CreateExam(examDTO);
            if (exam == null)
                return NotFound();

            return CreatedAtAction(nameof(GetExamByID), new { id = exam.Id }, exam);
        }

        [HttpGet]
        public IEnumerable<Exam> GetEvents([FromQuery] int skip = 0, [FromQuery] int take = 20)
        {
            return _examService.GetExams(skip, take);
        }

        [HttpGet("doctor/{id}")]
        public IEnumerable<Exam> GetEventsFromDoctor(int id)
        {
            return _examService.GetExamsFromDoctor(id);
        }

        [HttpGet("user/{id}")]
        public IEnumerable<Exam> GetEventsFromUser(int id)
        {
            return _examService.GetExamsFromUser(id);
        }

        [HttpGet("{id}")]
        public IActionResult GetExamByID(int id)
        {
            Exam? exam = _examService.GetExamByID(id);
            if (exam == null)
                return NotFound();

            return Ok(exam);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEvent(int id, [FromBody] UpdateExamDTO updatedExam)
        {
            Exam? exam = _examService.GetExamByID(id);
            if (exam == null)
                return NotFound();

            _examService.UpdateExam(id, updatedExam);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            Exam? exam = _examService.GetExamByID(id);
            if (exam == null)
                return NotFound();

            _examService.DeleteExam(exam);
            return NoContent();
        }
    }
}
