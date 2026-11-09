using FluentValidation;
using OnelineLearning.DTOs.Enrollments;
namespace OnelineLearning.Validators.Enrollments
{
    public class CreateEnrollmentDtoValidator : AbstractValidator<CreateEnrollmentDto>
    {
        public CreateEnrollmentDtoValidator()
        {
            RuleFor(x => x.CourseId)
                .NotEmpty()
                .WithMessage("L'ID du cours est obligatoire");
            RuleFor(x => x.StudentId)
                .NotEmpty()
                .WithMessage("L'ID de l'etudiant est obligatoire");
        }
    
    }
}
