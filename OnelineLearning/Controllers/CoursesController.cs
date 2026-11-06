using Microsoft.AspNetCore.Mvc;
using OnelineLearning.DTOs.Courses;
using OnelineLearning.DTOs.Students;
using OnelineLearning.Models;
using OnelineLearning.Repositories.Interfaces;

namespace OnelineLearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;

        public CoursesController(
            ICourseRepository courseRepository,
            ICategoryRepository categoryRepository,
            IEnrollmentRepository enrollmentRepository)
        {
            _courseRepository = courseRepository;
            _categoryRepository = categoryRepository;
            _enrollmentRepository = enrollmentRepository;
        }

        // GET: api/courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAll()
        {
            var courses = await _courseRepository.GetAllAsync();
            return Ok(courses.Select(ToDto));
        }

        // GET: api/courses/id
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetById(Guid id)
        {
            var course = await _courseRepository.GetByIdAsync(id);

            if (course == null)
                return NotFound("Cours introuvable.");

            return Ok(ToDto(course));
        }

        // GET: api/courses/category/id
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetByCategory(Guid categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);

            if (category == null)
                return NotFound("Catégorie introuvable.");

            var courses = await _courseRepository.GetByCategoryIdAsync(categoryId);
            return Ok(courses.Select(ToDto));
        }

        // POST: api/courses
        [HttpPost]
        public async Task<ActionResult<CourseDto>> Create(CreateCourseDto dto)
        {
            if (await _categoryRepository.GetByIdAsync(dto.CategoryId) == null)
                return NotFound("Catégorie introuvable.");

            var course = new Course
            {
                CategoryId = dto.CategoryId,
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price
            };

            await _courseRepository.AddAsync(course);
            await _courseRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = course.CourseId }, ToDto(course));
        }

        // PUT: api/courses/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateCourseDto dto)
        {
            var course = await _courseRepository.GetByIdAsync(id);

            if (course == null)
                return NotFound("Cours introuvable.");

            if (await _categoryRepository.GetByIdAsync(dto.CategoryId) == null)
                return NotFound("Categorie introuvable.");

            course.CategoryId = dto.CategoryId;
            course.Title = dto.Title;
            course.Description = dto.Description;
            course.Price = dto.Price;

            _courseRepository.Update(course);
            await _courseRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/courses/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var course = await _courseRepository.GetByIdAsync(id);

            if (course == null)
                return NotFound("Cours introuvable.");

            var withEnrollments = await _courseRepository.GetWithEnrollmentsAsync(id);
            if (withEnrollments != null && (withEnrollments.Enrollments.Any() == true))
                return Conflict("Impossible de supprimer un cours qui a des inscriptions.");

            _courseRepository.Delete(course);
            await _courseRepository.SaveChangesAsync();

            return NoContent();
        }

        private static CourseDto ToDto(Course c) => new()
        {
            CourseId = c.CourseId,
            CategoryId = c.CategoryId,
            Title = c.Title,
            Description = c.Description,
            Price = c.Price
        };
    }
}
