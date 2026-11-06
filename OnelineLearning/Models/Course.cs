namespace OnelineLearning.Models
{
    public class Course
    {
        public Guid CourseId { get; set; } = Guid.NewGuid();
        public Guid CategoryId { get; set; }
        public string Title { get; set; }   = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; } = null!;
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
