using Microsoft.AspNetCore.Mvc;
using OnelineLearning.DTOs.Courses;
using OnelineLearning.DTOs.StudentProfiles;
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
        private readonly IStudentProfileRepository _profileRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;

        public StudentsController(IStudentRepository studentRepository,IStudentProfileRepository profileRepository,IEnrollmentRepository enrollmentRepository)
        {
            this._studentRepository = studentRepository;
            this._profileRepository = profileRepository;
            this._enrollmentRepository = enrollmentRepository;
        }

        // GET: api/students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAll()
        {
            var students = await _studentRepository.GetAllAsync();
            return Ok(students.Select(ToDto));
        }

        // GET: api/students/id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<StudentDto>> GetById(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);

            if (student == null)
                return NotFound("etudiant nonn trouvé.");

            return Ok(ToDto(student));
        }

        //GET: api/students/email/
        [HttpGet("email/{email}")]
        public async Task<ActionResult<StudentDto>> GetByEmail(string email)
        {
            var student = await _studentRepository.GetByEmailAsync(email);

            if (student == null)
                return NotFound("Étudiant introuvable.");

            return Ok(ToDto(student));
        }

        // GET: api/students/id/profile
        [HttpGet("{id:int}/profile")]
        public async Task<ActionResult<StudentProfileDto>> GetProfile(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);

            if (student == null)
                return NotFound("etudiant non trouvé.");

            var profile = await _profileRepository.GetByStudentIdAsync(id);

            if (profile == null)
                return NotFound("Cet étudiant n'a pas encore de profil.");

            return Ok(new StudentProfileDto
            {
                StudentProfileId = profile.StudentProfileId,
                StudentId = profile.StudentId,
                PhoneNumber = profile.PhoneNumber,
                Address = profile.Address,
                BirthDate = profile.BirthDate
            });
        }

        // GET: api/students/id/courses 
        [HttpGet("{id:int}/courses")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);

            if (student == null)
                return NotFound("Étudiant introuvable.");

            var enrollments = await _enrollmentRepository.GetByStudentIdAsync(id);

            return Ok(enrollments
                .Where(e => e.Course != null)
                .Select(e => new CourseDto
                {
                    CourseId = e.Course!.CourseId,
                    CategoryId = e.Course.CategoryId,
                    Title = e.Course.Title,
                    Description = e.Course.Description,
                    Price = e.Course.Price
                }));
        }

        // POST: api/students
        [HttpPost]
        public async Task<ActionResult<StudentDto>> Create(CreateStudentDto dto)
        {
            if (await _studentRepository.GetByEmailAsync(dto.Email) != null)
                return Conflict("Cet email est déjà utilisé.");

            var student = new Student
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };

            await _studentRepository.AddAsync(student);
            await _studentRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = student.StudentId }, ToDto(student));
        }

        // PUT: api/students/id
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateStudentDto dto)
        {
            var student = await _studentRepository.GetByIdAsync(id);

            if (student == null)
                return NotFound("Étudiant introuvable.");

            var sameEmail = await _studentRepository.GetByEmailAsync(dto.Email);
            if (sameEmail != null && sameEmail.StudentId != id)
                return Conflict("Cet email est déjà utilisé par un autre étudiant.");

            student.FirstName = dto.FirstName;
            student.LastName = dto.LastName;
            student.Email = dto.Email;

            _studentRepository.Update(student);
            await _studentRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/students/5  (le profil et les inscriptions partent en cascade)
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);

            if (student == null)
                return NotFound("Étudiant introuvable.");

            _studentRepository.Delete(student);
            await _studentRepository.SaveChangesAsync();

            return NoContent();
        }

        private static StudentDto ToDto(Student s) => new()
        {
            StudentId = s.StudentId,
            FirstName = s.FirstName,
            LastName = s.LastName,
            Email = s.Email,
            CreatedAt = s.CreatedAt
        };
    }
}
