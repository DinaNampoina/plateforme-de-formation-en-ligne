namespace OnelineLearning.Models
{
    public class StudentProfile
    {
        public int StudentProfileId { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; } 
        public Student? Student { get; set; } = null;
    }
}
