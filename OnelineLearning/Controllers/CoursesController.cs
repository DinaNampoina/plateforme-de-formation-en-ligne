using Microsoft.AspNetCore.Mvc;
using OnelineLearning.Models;
using OnelineLearning.DTOs.Courses;
using OnelineLearning.Repositories.Interfaces;

namespace OnelineLearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CoursesController(
            ICourseRepository courseRepository,
            ICategoryRepository categoryRepository)
        {
            _courseRepository = courseRepository;
            _categoryRepository = categoryRepository;
        }

        // GET: api/courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAll()
        {
            var courses = await _courseRepository.GetAllAsync();

            var result = courses.Select(c => new CourseDto
            {
                CourseId = c.CourseId,
                CategoryId = c.CategoryId,
                Title = c.Title,
                Description = c.Description,
                Price = c.Price
            });

            return Ok(result);
        }

        // GET: api/courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetById(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);

            if (course == null)
                return NotFound("Cours introuvable.");

            var result = new CourseDto
            {
                CourseId = course.CourseId,
                CategoryId = course.CategoryId,
                Title = course.Title,
                Description = course.Description,
                Price = course.Price
            };

            return Ok(result);
        }

        // GET: api/courses/category/2
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetByCategory(int categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);

            if (category == null)
                return NotFound("Catégorie introuvable.");

            var courses = await _courseRepository.GetByCategoryIdAsync(categoryId);

            var result = courses.Select(c => new CourseDto
            {
                CourseId = c.CourseId,
                CategoryId = c.CategoryId,
                Title = c.Title,
                Description = c.Description,
                Price = c.Price
            });

            return Ok(result);
        }

        // POST: api/courses
        [HttpPost]
        public async Task<ActionResult<CourseDto>> Create(CreateCourseDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);

            if (category == null)
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

            var result = new CourseDto
            {
                CourseId = course.CourseId,
                CategoryId = course.CategoryId,
                Title = course.Title,
                Description = course.Description,
                Price = course.Price
            };

            return CreatedAtAction(
                nameof(GetById),
                new { id = course.CourseId },
                result
            );
        }

        // PUT: api/courses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCourseDto dto)
        {
            var course = await _courseRepository.GetByIdAsync(id);

            if (course == null)
                return NotFound("Cours introuvable.");

            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);

            if (category == null)
                return NotFound("Catégorie introuvable.");

            course.CategoryId = dto.CategoryId;
            course.Title = dto.Title;
            course.Description = dto.Description;
            course.Price = dto.Price;

            _courseRepository.Update(course);
            await _courseRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);

            if (course == null)
                return NotFound("Cours introuvable.");

            _courseRepository.Delete(course);
            await _courseRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}