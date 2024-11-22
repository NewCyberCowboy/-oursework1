using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coursework.Models
{
    public class Staff
    {
        public int StaffId { get; set; }
        public string? Name { get; set; }
        public string? Position { get; set; }
    }
    public class StaffUpdateDto
    {
        public string? Name { get; set; }
        public string? Position { get; set; }
    }
}
