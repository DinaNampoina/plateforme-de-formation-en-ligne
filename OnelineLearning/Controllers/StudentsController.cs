using Microsoft.AspNetCore.Mvc;
using OnelineLearning.DTOs;
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

        public StudentsController(IStudentRepository studentRepository, IStudentProfileRepository profileRepository, IEnrollmentRepository enrollmentRepository)
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
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<StudentDto>> GetById(Guid id)
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
        [HttpGet("{id:Guid}/profile")]
        public async Task<ActionResult<StudentProfileDto>> GetProfile(Guid id)
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
        [HttpGet("{id:Guid}/courses")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses(Guid id)
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
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateStudentDto dto)
        {
            var student = await _studentRepository.GetByIdAsync(id);

            if (student == null)
                return NotFound("etudiant introuvable");

            var sameEmail = await _studentRepository.GetByEmailAsync(dto.Email);
            if (sameEmail != null && sameEmail.StudentId != id)
                return Conflict("Cet email est deja utilisé par un autre etudiant");

            student.FirstName = dto.FirstName;
            student.LastName = dto.LastName;
            student.Email = dto.Email;

            _studentRepository.Update(student);
            await _studentRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/students/id  
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
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


        // GET: api/students/nom
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<StudentDto>>> SearchName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Le nom à rechercher est obligatoire.");
            }

            var students = await _studentRepository.SearchByNameAsync(name);

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

        // GET: api/students/pagination?pageNumber=1&pageSize=10
        [HttpGet("pagination")]
        public async Task<ActionResult<Pagination<StudentDto>>> GetPaged([FromQuery] int pageNumber = 1,[FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest();
            }

            var pagination = await _studentRepository.GetPagedAsync(pageNumber, pageSize);

            var result = new Pagination<StudentDto>
            {
                Items = pagination.Items.Select(s => ToDto(s)).ToList(),
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalCount = pagination.TotalCount,
                TotalPages = pagination.TotalPages
            };

            return Ok(result);
        }
    }
}
