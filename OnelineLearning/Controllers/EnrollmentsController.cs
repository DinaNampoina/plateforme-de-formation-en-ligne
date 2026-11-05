using Microsoft.AspNetCore.Mvc;
using OnelineLearning.Models;
using OnelineLearning.DTOs.Enrollments;
using OnelineLearning.Repositories.Interfaces;

namespace OnelineLearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;

        public EnrollmentsController(
            IEnrollmentRepository enrollmentRepository,
            IStudentRepository studentRepository,
            ICourseRepository courseRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        // GET: api/enrollment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnrollmentDto>>> GetAll()
        {
            var enrollments = await _enrollmentRepository.GetAllAsync();

            var result = enrollments.Select(e => new EnrollmentDto
            {
                EnrollmentId = e.EnrollmentId,
                StudentId = e.StudentId,
                CourseId = e.CourseId,
                EnrollmentDate = e.EnrollmentDate,
                Status = e.Status
            });

            return Ok(result);
        }

        // GET: api/enrollment/id
        [HttpGet("{id}")]
        public async Task<ActionResult<EnrollmentDto>> GetById(int id)
        {
            var enrollment = await _enrollmentRepository.GetByIdAsync(id);

            if (enrollment == null)
                return NotFound("inscription introuvable.");

            var result = new EnrollmentDto
            {
                EnrollmentId = enrollment.EnrollmentId,
                StudentId = enrollment.StudentId,
                CourseId = enrollment.CourseId,
                EnrollmentDate = enrollment.EnrollmentDate,
                Status = enrollment.Status
            };

            return Ok(result);
        }

        // GET: api/enrollment/student/id
        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<IEnumerable<EnrollmentDto>>> GetByStudent(int studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);

            if (student == null)
                return NotFound("etudiant introuvable.");

            var enrollments = await _enrollmentRepository.GetByStudentIdAsync(studentId);

            var result = enrollments.Select(e => new EnrollmentDto
            {
                EnrollmentId = e.EnrollmentId,
                StudentId = e.StudentId,
                CourseId = e.CourseId,
                EnrollmentDate = e.EnrollmentDate,
                Status = e.Status
            });

            return Ok(result);
        }

        // GET: api/enrollments/course/id
        [HttpGet("course/{courseId}")]
        public async Task<ActionResult<IEnumerable<EnrollmentDto>>> GetByCourse(int courseId)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);

            if (course == null)
                return NotFound("Cours introuvable.");

            var enrollments = await _enrollmentRepository.GetByCourseIdAsync(courseId);

            var result = enrollments.Select(e => new EnrollmentDto
            {
                EnrollmentId = e.EnrollmentId,
                StudentId = e.StudentId,
                CourseId = e.CourseId,
                EnrollmentDate = e.EnrollmentDate,
                Status = e.Status
            });

            return Ok(result);
        }

        // POST: api/enrollments
        [HttpPost]
        public async Task<ActionResult<EnrollmentDto>> Create(CreateEnrollmentDto dto)
        {
            var student = await _studentRepository.GetByIdAsync(dto.StudentId);

            if (student == null)
                return NotFound("Étudiant introuvable.");

            var course = await _courseRepository.GetByIdAsync(dto.CourseId);

            if (course == null)
                return NotFound("Cours introuvable.");

            var existingEnrollment =
                await _enrollmentRepository.GetByStudentAndCourseAsync(
                    dto.StudentId,
                    dto.CourseId
                );

            if (existingEnrollment != null)
                return Conflict("Cet étudiant est déjà inscrit à ce cours.");

            var enrollment = new Enrollment
            {
                StudentId = dto.StudentId,
                CourseId = dto.CourseId,
                Status = dto.Status
                // EnrollmentDate = DateTime.UtcNow est défini par défaut dans le modèle
            };

            await _enrollmentRepository.AddAsync(enrollment);
            await _enrollmentRepository.SaveChangesAsync();

            var result = new EnrollmentDto
            {
                EnrollmentId = enrollment.EnrollmentId,
                StudentId = enrollment.StudentId,
                CourseId = enrollment.CourseId,
                EnrollmentDate = enrollment.EnrollmentDate,
                Status = enrollment.Status
            };

            return CreatedAtAction(
                nameof(GetById),
                new { id = enrollment.EnrollmentId },
                result
            );
        }

        // PUT: api/enrollments/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateEnrollmentDto dto)
        {
            var enrollment = await _enrollmentRepository.GetByIdAsync(id);

            if (enrollment == null)
                return NotFound("Inscription introuvable.");

            enrollment.Status = dto.Status;

            _enrollmentRepository.Update(enrollment);
            await _enrollmentRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/enrollments/1  -> annuler une inscription
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var enrollment = await _enrollmentRepository.GetByIdAsync(id);

            if (enrollment == null)
                return NotFound("Inscription introuvable.");

            _enrollmentRepository.Delete(enrollment);
            await _enrollmentRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}