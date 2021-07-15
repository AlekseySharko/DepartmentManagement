using DepartmentManagementModels;
using Microsoft.EntityFrameworkCore;

namespace DepartmentManagementEfCore
{
    public class DepartmentManagementContext : DbContext
    {
        public DepartmentManagementContext(DbContextOptions<DepartmentManagementContext> opts)
            : base(opts) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SetUpDepartments();
            modelBuilder.SetUpEmployees();
        }
    }
}
