namespace OnelineLearning.DTOs.Courses
{
    public class CourseDto
    {
        public Guid CourseId { get; set; } 
        public Guid CategoryId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
