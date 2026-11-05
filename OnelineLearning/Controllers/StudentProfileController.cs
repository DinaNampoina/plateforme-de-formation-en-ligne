
using Microsoft.AspNetCore.Mvc;
using OnelineLearning.Models;
using OnelineLearning.DTOs.StudentProfiles;
using OnelineLearning.Repositories.Interfaces;

namespace OnelineLearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentProfilesController : ControllerBase
    {
        private readonly IStudentProfileRepository _profileRepository;
        private readonly IStudentRepository _studentRepository;

        public StudentProfilesController(IStudentProfileRepository profileRepository,IStudentRepository studentRepository)
        {
            this._profileRepository = profileRepository;
            this._studentRepository = studentRepository;
        }

        // GET: api/studentprofiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentProfileDto>>> GetAll()
        {
            var profiles = await _profileRepository.GetAllAsync();

            var result = profiles.Select(p => new StudentProfileDto
            {
                StudentProfileId = p.StudentProfileId,
                StudentId = p.StudentId,
                PhoneNumber = p.PhoneNumber,
                Address = p.Address,
                BirthDate = p.BirthDate
            });

            return Ok(result);
        }

        // GET: api/studentprofiles/id
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentProfileDto>> GetById(int id)
        {
            var profile = await _profileRepository.GetByIdAsync(id);

            if (profile == null)
            {
                return NotFound("Profil non trouvee");
            }

            var result = new StudentProfileDto
            {
                StudentProfileId = profile.StudentProfileId,
                StudentId = profile.StudentId,
                PhoneNumber = profile.PhoneNumber,
                Address = profile.Address,
                BirthDate = profile.BirthDate
            };

            return Ok(result);
        }

        // GET: api/studentprofiles/student/1
        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<StudentProfileDto>> GetByStudentId(int studentId)
        {
            var profile = await _profileRepository.GetByStudentIdAsync(studentId);

            if (profile == null)
            {
                return NotFound("Profil non trouvée");
            }

            var result = new StudentProfileDto
            {
                StudentProfileId = profile.StudentProfileId,
                StudentId = profile.StudentId,
                PhoneNumber = profile.PhoneNumber,
                Address = profile.Address,
                BirthDate = profile.BirthDate
            };

            return Ok(result);
        }

        // POST: api/studentprofile
        [HttpPost]
        public async Task<ActionResult<StudentProfileDto>> Create(
            CreateStudentProfileDto dto)
        {
            var student = await _studentRepository.GetByIdAsync(dto.StudentId);

            if (student == null)
            {
                return NotFound("Student introuvable.");
            }

            var existingProfile =
                await _profileRepository.GetByStudentIdAsync(dto.StudentId);

            if (existingProfile != null)
            {
                return Conflict("Cet étudiant possède déjà un profil.");
            }

            var profile = new StudentProfile
            {
                StudentId = dto.StudentId,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                BirthDate = dto.BirthDate
            };

            await _profileRepository.AddAsync(profile);
            await _profileRepository.SaveChangesAsync();

            var result = new StudentProfileDto
            {
                StudentProfileId = profile.StudentProfileId,
                StudentId = profile.StudentId,
                PhoneNumber = profile.PhoneNumber,
                Address = profile.Address,
                BirthDate = profile.BirthDate
            };

            return Ok(result);
        }

        // PUT: api/studentprofiles/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateStudentProfileDto dto)
        {
            var profile = await _profileRepository.GetByIdAsync(id);

            if (profile == null)
            {
                return NotFound();
            }

            profile.PhoneNumber = dto.PhoneNumber;
            profile.Address = dto.Address;
            profile.BirthDate = dto.BirthDate;

            _profileRepository.Update(profile);
            await _profileRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/studentprofiles/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var profile = await _profileRepository.GetByIdAsync(id);

            if (profile == null)
            {
                return NotFound();
            }

            _profileRepository.Delete(profile);
            await _profileRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
