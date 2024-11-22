using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coursework.Models

{
    public class PrescriptionMedicine
    {
        public int PrescriptionId { get; set; }

        public int MedicineId { get; set; }
    }
}
