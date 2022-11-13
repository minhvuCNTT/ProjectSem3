using System;
using System.Collections.Generic;

namespace Pro3MVC.Models
{
    public partial class BatchSubject
    {
        public int Id { get; set; }
        public int? BatchId { get; set; }
        public int? SubjectId { get; set; }

        public virtual Batch? Batch { get; set; }
        public virtual Subject? Subject { get; set; }
    }
}
