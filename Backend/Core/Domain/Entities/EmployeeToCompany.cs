using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
    public class EmployeeToCompany
    {
        public int ID { get; set; }
        
        public int EmployeeID { get; set; }
        
        public int CompanyID { get; set; }
        
        [ForeignKey("EmployeeID")]
        public required Employee Employee { get; set; }
        
        [ForeignKey("CompanyID")]
        public required Company Company { get; set; }
    }
}
