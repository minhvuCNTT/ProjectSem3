using System;
using System.Collections.Generic;

namespace Pro3MVC.Models
{
    public partial class Batch
    {
        public Batch()
        {
            BatchSubjects = new HashSet<BatchSubject>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? CourseId { get; set; }
        public bool? Status { get; set; }

        public virtual Course? Course { get; set; }
        public virtual ICollection<BatchSubject> BatchSubjects { get; set; }
    }
}
