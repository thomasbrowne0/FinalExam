using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Entities
{
    public class Employee
    {
        public Employee()
        {
            Companies = new HashSet<EmployeeToCompany>();
            CreatedShifts = new HashSet<Shift>();
            AssignedShifts = new HashSet<Shift>();
            ShiftAssignments = new HashSet<EmployeeToShift>();
            AdminForCompanies = new HashSet<Company>();
            
            // Initialize required properties with default values
            Name = string.Empty;
            Address = string.Empty;
            Role = "employee";
        }

        public int EmployeeID { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        public DateTime? Birthday { get; set; }
        
        [MaxLength(255)]
        public string Address { get; set; }
        
        [MaxLength(20)]
        public string Role { get; set; }
        
        // Navigation properties
        public virtual ICollection<EmployeeToCompany> Companies { get; set; }
        
        // Shifts where this employee is listed as the creator
        public virtual ICollection<Shift> CreatedShifts { get; set; }
        
        // Shifts directly assigned to this employee (via Shift.EmployeeID)
        public virtual ICollection<Shift> AssignedShifts { get; set; }
        
        // Many-to-many shift assignments through junction table
        public virtual ICollection<EmployeeToShift> ShiftAssignments { get; set; }
        
        // Companies where this employee is an admin
        public virtual ICollection<Company> AdminForCompanies { get; set; }
    }
}
