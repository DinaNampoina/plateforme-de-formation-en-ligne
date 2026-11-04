namespace OnelineLearning.DTOs.StudentProfiles
{
    public class UpdateStudentProfileDto
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
    }
}