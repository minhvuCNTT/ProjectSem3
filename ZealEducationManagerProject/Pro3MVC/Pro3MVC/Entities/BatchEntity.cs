using Pro3MVC.Models;

namespace Pro3MVC.Entities
{
    public class BatchEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool Status { get; set; }
        public int CourseId { get; set; }
        public string? courseName { get; set; }
    }
}
