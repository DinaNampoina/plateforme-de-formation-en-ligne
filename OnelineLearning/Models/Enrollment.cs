namespace OnelineLearning.Models
{
    public class Enrollment
    {
        public enum EnrollmentStatus { 
            Active = 0,
            Completed = 1,
            Cancelled = 2
        }
         public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; } = null;
        public int CourseId { get; set; }
        public Course? Course { get; set; } = null;
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
        public EnrollmentStatus Status { get; set; }  = EnrollmentStatus.Active;


    }
}
