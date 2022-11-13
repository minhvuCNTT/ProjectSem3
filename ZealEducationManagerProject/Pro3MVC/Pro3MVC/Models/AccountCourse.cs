using System;
using System.Collections.Generic;

namespace Pro3MVC.Models
{
    public partial class AccountCourse
    {
        public int AccountId { get; set; }
        public int CourseId { get; set; }
        public DateTime? StartDate { get; set; }
        public decimal? Fee { get; set; }
        public bool? Status { get; set; }
        public string? Description { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual Course Course { get; set; } = null!;
    }
}
