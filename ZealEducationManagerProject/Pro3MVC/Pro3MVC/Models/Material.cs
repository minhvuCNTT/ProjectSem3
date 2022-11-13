using System;
using System.Collections.Generic;

namespace Pro3MVC.Models
{
    public partial class Material
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Link { get; set; }
        public string? Filename { get; set; }
        public int? SubjectId { get; set; }
        public bool? Status { get; set; }

        public virtual Subject? Subject { get; set; }
    }
}
