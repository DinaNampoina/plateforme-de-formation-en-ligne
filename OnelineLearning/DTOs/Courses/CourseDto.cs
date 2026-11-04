namespace OnelineLearning.DTOs.Courses
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
