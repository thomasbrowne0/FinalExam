using Microsoft.EntityFrameworkCore;
using Core.Domain.Entities;
using Core.Application.Interfaces;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<EmployeeToCompany> EmployeeToCompanies { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<EmployeeToShift> EmployeeToShifts { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure unique constraints and relationships
            modelBuilder.Entity<EmployeeToCompany>()
                .HasIndex(e => new { e.EmployeeID, e.CompanyID })
                .IsUnique();
                
            modelBuilder.Entity<EmployeeToShift>()
                .HasIndex(e => new { e.ShiftID, e.EmployeeID })
                .IsUnique();
            
            // Configure Employee to Shift relationships
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.AssignedShifts)
                .WithOne(s => s.AssignedEmployee)
                .HasForeignKey(s => s.EmployeeID)
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.CreatedShifts)
                .WithOne(s => s.CreatedBy)
                .HasForeignKey(s => s.CreatedByEmployeeID)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Configure many-to-many relationship via junction table
            modelBuilder.Entity<EmployeeToShift>()
                .HasOne(es => es.Employee)
                .WithMany(e => e.ShiftAssignments)
                .HasForeignKey(es => es.EmployeeID);
            
            modelBuilder.Entity<EmployeeToShift>()
                .HasOne(es => es.Shift)
                .WithMany(s => s.EmployeeAssignments)
                .HasForeignKey(es => es.ShiftID);
            
            // Configure Company-Admin relationship
            modelBuilder.Entity<Company>()
                .HasOne(c => c.Administrator)
                .WithMany(e => e.AdminForCompanies)
                .HasForeignKey(c => c.CompanyAdmin);
            
            // Configure check constraint for Shift using new syntax
            modelBuilder.Entity<Shift>()
                .ToTable(tb => tb.HasCheckConstraint("CK_Shift_EndTime_After_StartTime", "\"EndTime\" > \"StartTime\""));
                
            // Configure enum validation for Employee.Role using new syntax
            modelBuilder.Entity<Employee>()
                .ToTable(tb => tb.HasCheckConstraint("CK_Employee_Role", "\"Role\" IN ('employee', 'manager', 'CompanyAdmin')"));
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
