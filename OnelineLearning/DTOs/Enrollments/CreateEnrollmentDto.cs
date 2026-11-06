using OnelineLearning.Models;

namespace OnelineLearning.DTOs.Enrollments
{
    public class CreateEnrollmentDto
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public Enrollment.EnrollmentStatus Status { get; set; } = Enrollment.EnrollmentStatus.Active;
    }
}