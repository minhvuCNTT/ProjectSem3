using System;
using System.Collections.Generic;

namespace Pro3MVC.Models
{
    public partial class Enquiry
    {
        public int Id { get; set; }
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public int? CourseId { get; set; }

        public virtual Course? Course { get; set; }
    }
}
