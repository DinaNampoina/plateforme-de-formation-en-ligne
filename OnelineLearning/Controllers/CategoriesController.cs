using Microsoft.AspNetCore.Mvc;
using OnelineLearning.DTOs.Categories;
using OnelineLearning.DTOs.Courses;
using OnelineLearning.Models;
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
            this._categoryRepository = categoryRepository;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return Ok(categories.Select(ToDto));
        }

        // GET: api/categories/id
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                return NotFound("Catégorie introuvable.");

            return Ok(ToDto(category));
        }

        // GET: api/categories/id/courses
        [HttpGet("{id}/courses")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                return NotFound("Categorie introuvable.");

            var courses = await _categoryRepository.GetCoursesAsync(id);

            return Ok(courses.Select(c => new CourseDto
            {
                CourseId = c.CourseId,
                CategoryId = c.CategoryId,
                Title = c.Title,
                Description = c.Description,
                Price = c.Price
            }));
        }

        // POST: api/categories
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create(CreateCategoryDto dto)
        {
            if (await _categoryRepository.GetByNameAsync(dto.Name) != null)
                return Conflict("Cette catégorie existe déjà.");

            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description
            };

            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = category.CategoryId }, ToDto(category));
        }

        // PUT: api/categories/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCategoryDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                return NotFound("categorie introuvable");

            var sameName = await _categoryRepository.GetByNameAsync(dto.Name);
            if (sameName != null && sameName.CategoryId != id)
                return Conflict("un  categorie a deja ce nom");

            category.Name = dto.Name;
            category.Description = dto.Description;

            _categoryRepository.Update(category);
            await _categoryRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/categories/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                return NotFound("Catégorie introuvable.");

            var courses = await _categoryRepository.GetCoursesAsync(id);
            if (courses.Any() == true)
                return Conflict("Impossible de supprimer ce  categorie qui contient des cours.");

            _categoryRepository.Delete(category);
            await _categoryRepository.SaveChangesAsync();

            return NoContent();
        }

        private static CategoryDto ToDto(Category c) => new()
        {
            CategoryId = c.CategoryId,
            Name = c.Name,
            Description = c.Description
        };
    }
}
