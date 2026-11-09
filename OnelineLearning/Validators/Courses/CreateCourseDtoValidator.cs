using FluentValidation;
using OnelineLearning.DTOs.Courses;
namespace OnelineLearning.Validators.Courses
{
    public class CreateCourseDtoValidator : AbstractValidator<CreateCourseDto>
    {
        public CreateCourseDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty()
                .WithMessage("Le titre du cours est obligatoire")
                .MaximumLength(200);
            RuleFor(x => x.Description)
                .MaximumLength(1000);
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Le prix du cours doit être supérieur ou égal à zéro ");
            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage("L'ID de la catégorie est obligatoire.");
        }
    
    }
}
