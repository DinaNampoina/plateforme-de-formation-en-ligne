namespace OnelineLearning.DTOs.Students
{
    public class CreateStudentDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int StudentId { get; internal set; }
        public DateTime CreatedAt { get; internal set; }

    }
}