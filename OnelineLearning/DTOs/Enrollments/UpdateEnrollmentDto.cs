using OnelineLearning.Models;

namespace OnelineLearning.DTOs.Enrollments
{
    public class UpdateEnrollmentDto
    {
        public Enrollment.EnrollmentStatus Status { get; set; } = Enrollment.EnrollmentStatus.Active;
    }
}
