namespace Pro3MVC.Entities
{
    public class SubjectEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Sessions { get; set; }
        public string? batchName { get; set; }
        public string? courseName { get; set; }
        public string? facultyName { get; set; }
        public bool? Status { get; set; }

        public int? IdC { get; set; }

    }
}
