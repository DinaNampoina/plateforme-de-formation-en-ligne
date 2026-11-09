using FluentValidation;
using OnelineLearning.DTOs.Categories;

namespace OnelineLearning.Validators.Categories
{
    public class CreateCategoryDtoValidator
        : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Le nom de la catégorie est obligatoire.")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(500);
        }
    }
}