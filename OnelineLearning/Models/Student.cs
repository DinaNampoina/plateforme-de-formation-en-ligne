namespace OnelineLearning.Models
{
    public class Student
    {
        public Guid StudentId { get; set; } = Guid.NewGuid();   
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public StudentProfile? StudentProfile { get; set; } 
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
