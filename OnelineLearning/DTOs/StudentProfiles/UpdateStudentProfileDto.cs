using System.ComponentModel.DataAnnotations;

namespace OnelineLearning.DTOs.StudentProfiles
{
    public class UpdateStudentProfileDto
    {
        [StringLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
    }
}