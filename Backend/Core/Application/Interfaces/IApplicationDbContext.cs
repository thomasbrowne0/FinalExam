using Microsoft.EntityFrameworkCore;
using Core.Domain.Entities;

namespace Core.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Employee> Employees { get; set; }
        DbSet<Company> Companies { get; set; }
        DbSet<EmployeeToCompany> EmployeeToCompanies { get; set; }
        DbSet<Shift> Shifts { get; set; }
        DbSet<EmployeeToShift> EmployeeToShifts { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
