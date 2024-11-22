using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coursework.Models
{
    public class Medicine
    {
        public int MedicineId { get; set; }
        public string? Name { get; set; }
        public string? UsageMethod { get; set; }

        public string? Dosage { get; set; }

        public int? Quantity { get; set; }
    }
    public class MedicineUpdateDto
    {
        public string? Name { get; set; }

        public string? UsageMethod { get; set; }

        public string? Dosage { get; set; }

        public int? Quantity { get; set; }
    }
}
