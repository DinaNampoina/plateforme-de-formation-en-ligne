using System.ComponentModel.DataAnnotations;

namespace OnelineLearning.Models
{
    public class Course
    {
        public int IdCourse { get; set; }
        public int IdCategory { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
