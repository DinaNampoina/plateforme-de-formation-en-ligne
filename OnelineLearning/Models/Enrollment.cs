namespace OnelineLearning.Models
{
    public class Enrollment
    {
         public int IdEnrollment { get; set; }
        public int IdStudent { get; set; }
        public int IdCourse { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string Status { get; set; }
    }
}
