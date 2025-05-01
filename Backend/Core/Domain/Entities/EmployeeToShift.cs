using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
    public class EmployeeToShift
    {
        public int ID { get; set; }
        
        public int ShiftID { get; set; }
        
        public int EmployeeID { get; set; }
        
        [ForeignKey("ShiftID")]
        public required Shift Shift { get; set; }
        
        [ForeignKey("EmployeeID")]
        public required Employee Employee { get; set; }
    }
}
