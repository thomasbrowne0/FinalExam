using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
    public class Company
    {
        public Company()
        {
            CompanyName = string.Empty;
            Address = string.Empty;
            Employees = new HashSet<EmployeeToCompany>();
        }

        public int CompanyID { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string CompanyName { get; set; }
        
        [MaxLength(255)]
        public string Address { get; set; }
        
        public int? CompanyAdmin { get; set; }
        
        [ForeignKey("CompanyAdmin")]
        public Employee? Administrator { get; set; }
        
        // Navigation property
        public ICollection<EmployeeToCompany> Employees { get; set; }
    }
}
