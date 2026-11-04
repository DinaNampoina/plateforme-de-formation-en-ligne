namespace OnelineLearning.DTOs.Courses
{
    public class UpdateCourseDto
    {
        public int CategoryId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}