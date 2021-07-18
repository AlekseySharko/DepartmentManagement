using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DepartmentManagementModels;
using DepartmentManagementModels.OperationResults;
using DepartmentManagementModels.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DepartmentManagementEfCore.Repositories
{
    public class EfDepartmentRepository : IDepartmentRepository
    {
        private DepartmentManagementContext DepartmentManagementContext { get; }
        public EfDepartmentRepository(DepartmentManagementContext context) => DepartmentManagementContext = context;

        public IQueryable<Department> GetDepartments(bool includeEmployees = false,
            Expression<Func<Department, bool>> whereExpression = null)
        {
            IQueryable<Department> departments = includeEmployees
                ? DepartmentManagementContext.Departments.Include(d => d.Employees).AsNoTracking()
                : DepartmentManagementContext.Departments.AsNoTracking();

            if (whereExpression != null)
            {
                departments = departments.Where(whereExpression);
            }

            return departments;
        }

        public async Task<OperationResult> AddDepartmentAsync(Department department)
        {
            return await EfRepoHelper.InvokeManagingExceptions(async () =>
            {
                department.WasAddedDate = DateTime.Now;
                DepartmentManagementContext.Departments.Add(department);
                await DepartmentManagementContext.SaveChangesAsync();
            }, "Ошибка при добавлении отдела");
        }

        public async Task<OperationResult> EditDepartmentAsync(Department department)
        {
            return await EfRepoHelper.InvokeManagingExceptions(async () =>
            {
                Department departmentToEdit =
                    DepartmentManagementContext.Departments.FirstOrDefault(d =>
                        d.DepartmentId == department.DepartmentId);

                if (departmentToEdit is null)
                {
                    throw new DbUpdateException("Such department doesn't exist");
                }

                departmentToEdit.Name = department.Name;
                departmentToEdit.WasChangedDate = DateTime.Now;
                
                await DepartmentManagementContext.SaveChangesAsync();
            }, "Ошибка при изменении отдела");
        }

        public async Task<OperationResult> DeleteDepartmentAsync(Department department)
        {
            return await EfRepoHelper.InvokeManagingExceptions(async () =>
            {
                Department departmentToDelete =
                    DepartmentManagementContext.Departments.Include(d => d.Employees)
                        .FirstOrDefault(d => d.DepartmentId == department.DepartmentId) ?? department;
                departmentToDelete.Employees = null;
                await DepartmentManagementContext.SaveChangesAsync();
                DepartmentManagementContext.Departments.Remove(departmentToDelete);
                await DepartmentManagementContext.SaveChangesAsync();
            }, "Ошибка при удалении отдела");
        }

        public IEnumerable<string> GetExistingPositions(long departmentId)
        {
            return DepartmentManagementContext.Departments
                .Include(d => d.Employees).AsNoTracking()
                .FirstOrDefault(d => d.DepartmentId == departmentId)?.Employees?
                .Select(e => e.Position).Distinct();
        }
    }
}
