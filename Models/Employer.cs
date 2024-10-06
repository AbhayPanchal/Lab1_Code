using System.ComponentModel.DataAnnotations;

namespace Lab1.Models
{
    public class Employer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Website { get; set; }

        [Display(Name = "Incorporated Date")]
        [DataType(DataType.Date)]
        public DateTime? IncorporatedDate { get; set; }
    }
}
