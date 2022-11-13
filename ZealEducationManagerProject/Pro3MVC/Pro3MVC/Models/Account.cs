using System;
using System.Collections.Generic;

namespace Pro3MVC.Models
{
    public partial class Account
    {
        public Account()
        {
            AccountCourses = new HashSet<AccountCourse>();
            AccountSubjects = new HashSet<AccountSubject>();
            Invoices = new HashSet<Invoice>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string? Password { get; set; }
        public DateTime? Dob { get; set; }
        public string? Email { get; set; }
        public string? Photo { get; set; }
        public bool Status { get; set; }
        public string? Role { get; set; }
        public string? Fullname { get; set; }

        public virtual ICollection<AccountCourse> AccountCourses { get; set; }
        public virtual ICollection<AccountSubject> AccountSubjects { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
