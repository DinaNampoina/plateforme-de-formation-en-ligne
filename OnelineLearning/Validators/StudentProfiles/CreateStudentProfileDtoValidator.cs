using FluentValidation;
using OnelineLearning.DTOs.StudentProfiles;
namespace OnelineLearning.Validators.StudentProfiles
{
    public class CreateStudentProfileDtoValidator : AbstractValidator<CreateStudentProfileDto>
    {
        public CreateStudentProfileDtoValidator()
        {
            RuleFor(x => x.StudentId)
                .NotEmpty()
                .WithMessage("L'ID de l'étudiant est obligatoire");
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Le numéro de téléphone est obligatoire")
                .MaximumLength(10);
            RuleFor(x => x.Address).NotEmpty()
                .WithMessage("L'adresse est obligatoire")
                .MaximumLength(200);
            RuleFor(date => date.BirthDate)
                 .NotEmpty()
                 .WithMessage("La date de naissance est obligatoire")
                 .LessThan(DateTime.Now)
                 .WithMessage("La date de naissance doit être antérieure a la date actuelle");

        }
    }
}
