using static OnelineLearning.Models.Enrollment;
namespace OnelineLearning.DTOs.Enrollments
{
    public class EnrollmentDto
    {
        public Guid EnrollmentId { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public EnrollmentStatus Status { get; set; }
    }
}
