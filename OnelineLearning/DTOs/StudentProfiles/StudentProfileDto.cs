namespace OnelineLearning.DTOs.StudentProfiles
{
    public class StudentProfileDto
    {
        public int StudentProfileId { get; set; }
        public int StudentId { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
    }
}