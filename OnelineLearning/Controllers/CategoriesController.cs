
using Microsoft.AspNetCore.Mvc;
using OnelineLearning.Models;
using OnelineLearning.DTOs.Categories;
using OnelineLearning.DTOs.Courses;
using OnelineLearning.Repositories.Interfaces;

namespace OnelineLearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
        {
            var categories = await _categoryRepository.GetAllAsync();

            var result = categories.Select(c => new CategoryDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description
            });

            return Ok(result);
        }

        // GET: api/categories/1
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var result = new CategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description
            };

            return Ok(result);
        }

        // GET: api/categories/1/courses
        [HttpGet("{id}/courses")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound("Catégorie introuvable.");
            }

            var courses = await _categoryRepository.GetCoursesAsync(id);

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

        // POST: api/categories
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create(CreateCategoryDto dto)
        {
            var existingCategory =
                await _categoryRepository.GetByNameAsync(dto.Name);

            if (existingCategory != null)
            {
                return Conflict("Cette catégorie existe déjà.");
            }

            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description
            };

            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();

            var result = new CategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description
            };

            return CreatedAtAction(
                nameof(GetById),
                new { id = category.CategoryId },
                result
            );
        }

        // PUT: api/categories/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateCategoryDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            category.Name = dto.Name;
            category.Description = dto.Description;

            _categoryRepository.Update(category);
            await _categoryRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/categories/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            _categoryRepository.Delete(category);
            await _categoryRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
