using OnelineLearning.Models;

namespace OnelineLearning.DTOs.Enrollments
{
    public class CreateEnrollmentDto
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public Enrollment.EnrollmentStatus Status { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}