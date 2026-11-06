using System.ComponentModel.DataAnnotations;

namespace OnelineLearning.DTOs.StudentProfiles
{
    public class CreateStudentProfileDto
    {
        public Guid StudentId { get; set; }
        [StringLength(10)]
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
    }
}