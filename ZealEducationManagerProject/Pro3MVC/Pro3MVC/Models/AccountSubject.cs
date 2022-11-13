using System;
using System.Collections.Generic;

namespace Pro3MVC.Models
{
    public partial class AccountSubject
    {
        public int Id { get; set; }
        public int? AccountId { get; set; }
        public int? SubjectId { get; set; }
        public decimal? Score { get; set; }
        public int? CourseId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Subject? Subject { get; set; }
    }
}
