using FluentValidation;
using OnelineLearning.DTOs.Enrollments;
namespace OnelineLearning.Validators.Enrollments
{
    public class UpdateEnrollmentDtoValidator : AbstractValidator<UpdateEnrollmentDto>
    {
        public UpdateEnrollmentDtoValidator()
        {
            RuleFor(x => x.Status).NotEmpty()
                .WithMessage("Le statut de l'inscription est obligatoire");
        }
    }
}
