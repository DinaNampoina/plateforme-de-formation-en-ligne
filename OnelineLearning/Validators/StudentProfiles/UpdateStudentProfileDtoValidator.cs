using FluentValidation;
using OnelineLearning.DTOs.StudentProfiles;
namespace OnelineLearning.Validators.StudentProfiles
{
    public class UpdateStudentProfileDtoValidator : AbstractValidator<UpdateStudentProfileDto>
    {
        public UpdateStudentProfileDtoValidator()
        {
            RuleFor(x =>x.Address)
                .NotEmpty()
                .WithMessage("L'adresse est obligatoire")
                .MaximumLength(200)
                .WithMessage("L'adresse ne peut pas dépasser 200 caractères");
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Le numéro de téléphone est obligatoire")
                .MaximumLength(10);
            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .WithMessage("La date de naissance est obligatoire")
                .LessThan(DateTime.Now)
                .WithMessage("La date de naissance doit être antérieure à la date actuelle");
        }
    }
}
