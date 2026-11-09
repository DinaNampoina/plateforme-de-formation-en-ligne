
using Microsoft.AspNetCore.Mvc;
using OnelineLearning.DTOs;
using OnelineLearning.DTOs.StudentProfiles;
using OnelineLearning.Models;
using OnelineLearning.Repositories.Interfaces;
using FluentValidation;

namespace OnelineLearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentProfilesController : ControllerBase
    {
        private readonly IStudentProfileRepository _profileRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IValidator<CreateStudentProfileDto> _createProfileValidator;
        private readonly IValidator<UpdateStudentProfileDto> _updateProfileValidator;

        public StudentProfilesController(IStudentProfileRepository profileRepository, IStudentRepository studentRepository, IValidator<CreateStudentProfileDto> createProfileValidator, IValidator<UpdateStudentProfileDto> updateProfileValidator)
        {
            this._profileRepository = profileRepository;
            this._studentRepository = studentRepository;
            this._createProfileValidator = createProfileValidator;
            this._updateProfileValidator = updateProfileValidator;
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
        public async Task<ActionResult<StudentProfileDto>> GetById(Guid id)
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
        public async Task<ActionResult<StudentProfileDto>> GetByStudentId(Guid studentId)
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
            var validation = await _createProfileValidator.ValidateAsync(dto);
            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return ValidationProblem(ModelState);
            }
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
        public async Task<IActionResult> Update(Guid id, UpdateStudentProfileDto dto)
        {
            var  validation = await _updateProfileValidator.ValidateAsync(dto);
            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return ValidationProblem(ModelState);
            }
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

        // DELETE: api/studentprofile/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
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

        private static StudentProfileDto ProToDto(StudentProfile p) => new()
        {
            StudentProfileId = p.StudentProfileId,
            StudentId = p.StudentId,
            PhoneNumber = p.PhoneNumber,
            Address = p.Address,
            BirthDate = p.BirthDate
        };

        //GET: api/studentprofiles/pagination?pageNumber=1&pageSize=10
        [HttpGet("pagination")]
        public async Task<ActionResult<Pagination<StudentProfile>>> GetPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest();
            }
            var pagination = await _profileRepository.PageProfileAsync(pageNumber, pageSize);

            return Ok(new Pagination<StudentProfileDto>
            {
                Items = pagination.Items.Select(p => ProToDto(p)).ToList(),
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalCount = pagination.TotalCount,
                TotalPages = pagination.TotalPages
            });
        }
    }
}
