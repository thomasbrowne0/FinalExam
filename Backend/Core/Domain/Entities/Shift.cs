using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
    public class Shift
    {
        public Shift()
        {
            Description = string.Empty;
            Location = string.Empty;
            EmployeeAssignments = new HashSet<EmployeeToShift>();
        }

        public int ShiftID { get; set; }
        
        public int? EmployeeID { get; set; }
        
        public string Description { get; set; }
        
        [Required]
        public DateTime StartTime { get; set; }
        
        [Required]
        public DateTime EndTime { get; set; }
        
        [MaxLength(255)]
        public string Location { get; set; }
        
        public int CreatedByEmployeeID { get; set; }
        
        public bool IsPublished { get; set; } = false;
        
        [ForeignKey("EmployeeID")]
        public Employee? AssignedEmployee { get; set; }
        
        [ForeignKey("CreatedByEmployeeID")]
        public Employee? CreatedBy { get; set; }
        
        // Navigation property
        public ICollection<EmployeeToShift> EmployeeAssignments { get; set; }
    }
}
