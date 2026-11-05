using System.ComponentModel.DataAnnotations;

namespace OnelineLearning.DTOs.Categories
{
    public class UpdateCategoryDto
    {
        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
    }
}
