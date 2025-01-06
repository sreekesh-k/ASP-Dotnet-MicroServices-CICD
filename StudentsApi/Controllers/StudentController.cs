using Microsoft.AspNetCore.Mvc;
using StudentsApi.Data;
using StudentsApi.Models;

namespace StudentsApi.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly StudentDbContext _context;

        public StudentController(StudentDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            try
            {
                var students = _context.students.ToList();
                if (!students.Any())
                {
                    return NotFound(new { message = "No students found." });
                }
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving students.", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            try
            {
                var student = _context.students.Find(id);
                if (student == null)
                {
                    return NotFound(new { message = $"Student with ID {id} not found." });
                }
                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the student.", details = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CreateItem([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest(new { message = "Invalid student data." });
            }

            try
            {
                _context.students.Add(student);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the student.", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest(new { message = "Invalid student data." });
            }

            try
            {
                var existingStudent = _context.students.Find(id);
                if (existingStudent == null)
                {
                    return NotFound(new { message = $"Student with ID {id} not found." });
                }

                existingStudent.StudentName = student.StudentName;
                existingStudent.Course = student.Course;
                existingStudent.Age = student.Age;
                existingStudent.Email = student.Email;

                _context.SaveChanges();
                return Ok(new { message = "Student updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the student.", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                var student = _context.students.Find(id);
                if (student == null)
                {
                    return NotFound(new { message = $"Student with ID {id} not found." });
                }

                _context.students.Remove(student);
                _context.SaveChanges();
                return Ok(new { message = "Student deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the student.", details = ex.Message });
            }
        }

        [HttpGet("search")]
        public IActionResult SearchStudents([FromQuery] string? course, [FromQuery] int? age, [FromQuery] string? name, [FromQuery] string? email)
        {
            try
            {
                var query = _context.students.AsQueryable();

                if (!string.IsNullOrWhiteSpace(course))
                {
                    string courseLower = course.ToLower();
                    query = query.Where(s => s.Course.ToLower() == courseLower);
                }

                if (age.HasValue)
                {
                    query = query.Where(s => s.Age == age.Value);
                }

                if (!string.IsNullOrWhiteSpace(name))
                {
                    string nameLower = name.ToLower();
                    query = query.Where(s => s.StudentName.ToLower().Contains(nameLower));
                }

                if (!string.IsNullOrWhiteSpace(email))
                {
                    string emailLower = email.ToLower();
                    query = query.Where(s => s.Email.ToLower() == emailLower);
                }

                var result = query.ToList();

                if (!result.Any())
                {
                    return NotFound(new { message = "No students found with the given criteria." });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while searching for students.", details = ex.Message });
            }
        }


    }
}
