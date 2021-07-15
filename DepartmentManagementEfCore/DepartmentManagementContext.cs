using DepartmentsAndEmployeesModels;
using Microsoft.EntityFrameworkCore;

namespace DepartmentsAndEmployeesEfCore
{
    public class DepartmentsAndEmployeesContext : DbContext
    {
        public DepartmentsAndEmployeesContext(DbContextOptions<DepartmentsAndEmployeesContext> opts)
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
