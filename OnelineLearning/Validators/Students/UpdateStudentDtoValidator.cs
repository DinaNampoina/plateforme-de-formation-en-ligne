using FluentValidation;
using OnelineLearning.DTOs.Students;

namespace OnelineLearning.Validators.Students
{
    public class UpdateStudentDtoValidator
        : AbstractValidator<UpdateStudentDto>
    {
        public UpdateStudentDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(150);
        }
    }
}