using System.ComponentModel.DataAnnotations;

namespace OnelineLearning.DTOs.Categories
{
    public class UpdateCategoryDto
    {
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      