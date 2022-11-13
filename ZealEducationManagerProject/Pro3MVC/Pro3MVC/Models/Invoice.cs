using System;
using System.Collections.Generic;

namespace Pro3MVC.Models
{
    public partial class Invoice
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? AccountId { get; set; }
        public int? CourseId { get; set; }
        public string? Payment { get; set; }
        public DateTime? Created { get; set; }
        public decimal? Total { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Course? Course { get; set; }
    }
}
