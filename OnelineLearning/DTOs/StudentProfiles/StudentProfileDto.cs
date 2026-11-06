namespace OnelineLearning.DTOs.StudentProfiles
{
    public class StudentProfileDto
    {
        public Guid StudentProfileId { get; set; }
        public Guid StudentId { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
    }
}