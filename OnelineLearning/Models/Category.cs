namespace OnelineLearning.Models
{
    public class Category
    {
        public Guid CategoryId { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }  = string.Empty;
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
