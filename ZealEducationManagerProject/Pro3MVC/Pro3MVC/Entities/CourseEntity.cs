using System.ComponentModel.DataAnnotations;

namespace Pro3MVC.Entities
{
    public class CourseEntity
    {
        
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter course!!!")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Please enter fee!!!")]
        public decimal? Fee { get; set; }
        public byte Status { get; set; }

        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public DateTime? startDate { get; set; }
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public DateTime? endDate { get; set; }

        public int IdE { get; set; }
        public string? Question { get; set; }
        public string? Answer { get; set; }
    }
}
