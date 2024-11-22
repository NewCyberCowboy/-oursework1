using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Coursework.Models;

namespace Coursework.Models
{
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PatientId { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Disease { get; set; }

      
    }
    public class PatientUpdateDto
    {
        public string? Name { get; set; }
        public string? Disease { get; set; }
    }
}
