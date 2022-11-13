namespace Pro3MVC.Entities
{
    public class CourseDetail
    {
        public string? BatchName { get; set; }
        public int? CourseId { get; set; }
        public string? CourseName { get; set; }
        public int? BatchId { get; set; }
        public int? SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public int? SubjectSessions { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}
