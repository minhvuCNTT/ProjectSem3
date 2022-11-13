using System;
using System.Collections.Generic;

namespace Pro3MVC.Models
{
    public partial class FacultySubject
    {
        public int Id { get; set; }
        public int? FacultyId { get; set; }
        public int? SubjectId { get; set; }

        public virtual Faculty? Faculty { get; set; }
        public virtual Subject? Subject { get; set; }
    }
}
