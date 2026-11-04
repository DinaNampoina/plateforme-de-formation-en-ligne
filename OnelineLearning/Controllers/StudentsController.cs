using Microsoft.AspNetCore.Mvc;
using OnelineLearning.DTOs.Students;
using OnelineLearning.Models;
using OnelineLearning.Repositories.Interfaces;

namespace OnelineLearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        // GET: api/students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAll()
        {
            var students = await _studentRepository.GetAllAsync();

            var result = students.Select(s => new StudentDto
            {
                StudentId = s.StudentId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                CreatedAt = s.CreatedAt
            });

            return Ok(result);
        }

        // GET: api/students/id
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetById(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            var result = new StudentDto
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                CreatedAt = student.CreatedAt
            };

            return Ok(result);
        }

        // GET: api/students/email
        [HttpGet("email/{email}")]
        public async Task<ActionResult<StudentDto>> GetByEmail(string email)
        {
            var student = await _studentRepository.GetByEmailAsync(email);

            if (student == null)
            {
                return NotFound();
            }

            var result = new StudentDto
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                CreatedAt = student.CreatedAt
            };

            return Ok(result);
        }

        // POST: api/students
        [HttpPost]
        public async Task<ActionResult<StudentDto>> Create(CreateStudentDto createStudentDto)
        {
            var student = new Student
            {
                FirstName = createStudentDto.FirstName,
                LastName = createStudentDto.LastName,
                Email = createStudentDto.Email
            };

            await _studentRepository.AddAsync(student);
            await _studentRepository.SaveChangesAsync();

            var result = new StudentDto
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                CreatedAt = student.CreatedAt
            };

            return CreatedAtAction(
                nameof(GetById),
                new { id = student.StudentId },
                result
            );
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,UpdateStudentDto updateStudentDto)
        {
            var student = await _studentRepository.GetByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            student.FirstName = updateStudentDto.FirstName;
            student.LastName = updateStudentDto.LastName;
            student.Email = updateStudentDto.Email;

            await _studentRepository.UpdateAsync(student);
            Console.WriteLine($"ID demandé : {id}, étudiant trouvé : {student != null}");
            await _studentRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            await _studentRepository.DeleteAsync(student);
            await _studentRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
