using System;
using System.Collections.Generic;

namespace Pro3MVC.Models
{
    public partial class Faculty
    {
        public Faculty()
        {
            FacultySubjects = new HashSet<FacultySubject>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? Dob { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }
        public string? Photo { get; set; }

        public virtual ICollection<FacultySubject> FacultySubjects { get; set; }
    }
}
