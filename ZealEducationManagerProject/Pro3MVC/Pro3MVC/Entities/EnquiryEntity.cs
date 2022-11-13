namespace Pro3MVC.Entities
{
    public class EnquiryEntity
    {
        public int IdE { get; set; }
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public int? CourseId { get; set; }
    }
}
