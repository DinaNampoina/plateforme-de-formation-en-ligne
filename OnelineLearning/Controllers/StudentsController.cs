using Microsoft.AspNetCore.Mvc;
using OnelineLearning.Repositories.Interfaces;

namespace OnelineLearning.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentRepository.GetAllAsync();

            return Ok(students);
        }
    }
}