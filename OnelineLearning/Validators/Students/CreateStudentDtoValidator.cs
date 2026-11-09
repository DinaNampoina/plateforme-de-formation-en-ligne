using FluentValidation;
using OnelineLearning.DTOs.Students;

namespace OnelineLearning.Validators.Students
{
    public class CreateStudentDtoValidator
        : AbstractValidator<CreateStudentDto>
    {
        public CreateStudentDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Le prénom est obligatoire.")
                .MaximumLength(100)
                .WithMessage("Le prénom ne doit pas dépasser 100 caractères.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Le nom est obligatoire.")
                .MaximumLength(100)
                .WithMessage("Le nom ne doit pas dépasser 100 caractères.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("L'adresse email est obligatoire.")
                .EmailAddress()
                .WithMessage("L'adresse email n'est pas valide.")
                .MaximumLength(150)
                .WithMessage("L'email ne doit pas dépasser 150 caractères.");
        }
    }
}