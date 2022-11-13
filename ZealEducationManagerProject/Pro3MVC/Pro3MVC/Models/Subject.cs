using System;
using System.Collections.Generic;

namespace Pro3MVC.Models
{
    public partial class Subject
    {
        public Subject()
        {
            AccountSubjects = new HashSet<AccountSubject>();
            BatchSubjects = new HashSet<BatchSubject>();
            FacultySubjects = new HashSet<FacultySubject>();
            Materials = new HashSet<Material>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Sessions { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<AccountSubject> AccountSubjects { get; set; }
        public virtual ICollection<BatchSubject> BatchSubjects { get; set; }
        public virtual ICollection<FacultySubject> FacultySubjects { get; set; }
        public virtual ICollection<Material> Materials { get; set; }
    }
}
