using System.ComponentModel.DataAnnotations;

namespace OnelineLearning.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public int CategoryId { get; set; } 
        public string Title { get; set; }   = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
