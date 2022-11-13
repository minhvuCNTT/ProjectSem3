using System;
using System.Collections.Generic;

namespace Pro3MVC.Models
{
    public partial class Course
    {
        public Course()
        {
            AccountCourses = new HashSet<AccountCourse>();
            Batches = new HashSet<Batch>();
            Enquiries = new HashSet<Enquiry>();
            Invoices = new HashSet<Invoice>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Fee { get; set; }
        public byte Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual ICollection<AccountCourse> AccountCourses { get; set; }
        public virtual ICollection<Batch> Batches { get; set; }
        public virtual ICollection<Enquiry> Enquiries { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
