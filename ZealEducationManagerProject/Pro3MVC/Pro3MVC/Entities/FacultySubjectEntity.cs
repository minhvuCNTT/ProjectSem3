namespace Pro3MVC.Entities
{
    public class FacultySubjectEntity
    {
        public int Id { get; set; }
        public int? FacultyId { get; set; }
        public int? SubjectId { get; set; }
        public string? SubjectName { get; set; }
    }
}
