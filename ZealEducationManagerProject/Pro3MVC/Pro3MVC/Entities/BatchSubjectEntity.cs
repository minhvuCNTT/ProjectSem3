namespace Pro3MVC.Entities
{
    public class BatchSubjectEntity
    {
        public int Id { get; set; }
        public int? SubjectId { get; set; }
        public int? BatchId { get; set; }
        public int? Sessions { get; set; }
        public string? subjectName { get; set; }
    }
}
