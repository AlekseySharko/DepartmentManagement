using DepartmentManagementModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DepartmentManagementEfCore
{
    static class DepartmentsAndEmployeesContextHelper
    {
        public static void SetUpDepartments(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .Property(d => d.Name)
                .HasColumnType("NVARCHAR(50)")
                .IsRequired();
            modelBuilder.Entity<Department>()
                .HasIndex(d => d.Name)
                .IsUnique();
            modelBuilder.Entity<Department>()
                .Property(d => d.WasAddedDate)
                .SetUpDateType()
                .IsRequired();
            modelBuilder.Entity<Department>()
                .Property(d => d.WasChangedDate)
                .SetUpDateType();
        }

        public static void SetUpEmployees(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(e => e.FullName)
                .HasColumnType("NVARCHAR(70)")
                .IsRequired();
            modelBuilder.Entity<Employee>()
                .Property(e => e.WasAddedDate)
                .SetUpDateType()
                .IsRequired();
            modelBuilder.Entity<Employee>()
                .Property(e => e.WasChangedDate)
                .SetUpDateType();
            modelBuilder.Entity<Employee>()
                .Property(e => e.WasEmployedDate)
                .SetUpDateType();
            modelBuilder.Entity<Employee>()
                .Property(e => e.Position)
                .HasColumnType("NVARCHAR(70)")
                .IsRequired();
        }

        private static PropertyBuilder<T> SetUpDateType<T>(this PropertyBuilder<T> property)
        {
            return property.HasColumnType("DATETIME2(2)");
        }
    }
}
