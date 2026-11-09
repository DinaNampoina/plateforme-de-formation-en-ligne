using FluentValidation;
using OnelineLearning.DTOs.Categories;

namespace OnelineLearning.Validators.Categories
{
    public class UpdateCategoryDtoValidator
        : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(500);
        }
    }
}