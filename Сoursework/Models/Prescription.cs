using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coursework.Models
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }
        public string? PrescriptionDuration { get; set; }
        public int? Quantity { get; set; }

        public int StaffId { get; set; }

        public int PatientId { get; set; }
    }
    public class PrescriptionUpdateDto
    {
        public string? PrescriptionDuration { get; set; }
        public int? Quantity { get; set; }
        public int StaffId { get; set; }
        public int PatientId { get; set; }
    }
}
