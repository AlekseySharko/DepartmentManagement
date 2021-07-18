using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DepartmentManagementModels;
using DepartmentManagementModels.OperationResults;
using DepartmentManagementModels.OperationResults.Generic;
using DepartmentManagementModels.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DepartmentManagementEfCore.Repositories
{
    public class EfEmployeeRepository : IEmployeeRepository
    {
        private DepartmentManagementContext DepartmentManagementContext { get; }
        public EfEmployeeRepository(DepartmentManagementContext context) => DepartmentManagementContext = context;
        
        public IQueryable<Employee> GetEmployees (bool includeDepartment = false, Expression<Func<Employee, bool>> whereExpression = null)
        {
            IQueryable<Employee> employees = includeDepartment
                ? DepartmentManagementContext.Employees.Include(e => e.Department).AsNoTracking()
                : DepartmentManagementContext.Employees.AsNoTracking();

            if (whereExpression != null)
            {
                employees = employees.Where(whereExpression);
            }

            return employees;
        }

        public async Task<OperationResult> AddEmployeeAsync(Employee employee)
        {
            return await EfRepoHelper.InvokeManagingExceptions(async () =>
            {
                employee.Department =
                    DepartmentManagementContext.Departments.FirstOrDefault(d =>
                        d.DepartmentId == employee.Department.DepartmentId);

                SetDepartmentChangedDateIfExists(employee.Department);
                employee.WasAddedDate = DateTime.Now;

                DepartmentManagementContext.Employees.Add(employee);
                await DepartmentManagementContext.SaveChangesAsync();
            }, "Ошибка при добавлении сотрудника");
        }

        public async Task<OperationResult> EditEmployeeAsync(Employee newEmployeeData)
        {
            if (newEmployeeData is null)
            {
                return OperationResult.Failed("Такого сотрудника не существует");
            }

            return await EfRepoHelper.InvokeManagingExceptions(async () =>
            {
                Employee employeeToEdit =
                    DepartmentManagementContext.Employees.FirstOrDefault(
                        e => e.EmployeeId == newEmployeeData.EmployeeId);

                if (employeeToEdit is null)
                {
                    throw new DbUpdateException("Such employee doesn't exist");
                }

                employeeToEdit.FullName = newEmployeeData.FullName;
                employeeToEdit.WasEmployedDate = newEmployeeData.WasEmployedDate;
                employeeToEdit.Position = newEmployeeData.Position;
                await ChangeDepartmentIfNeeded(employeeToEdit, newEmployeeData);
                employeeToEdit.WasChangedDate = DateTime.Now;

                await DepartmentManagementContext.SaveChangesAsync();
            }, "Ошибка при изменении сотрудника");
        }

        public async Task<OperationResult> DeleteEmployeeAsync(Employee employee)
        {
            return await EfRepoHelper.InvokeManagingExceptions(async () =>
            {
                Employee employeeToDelete =
                    DepartmentManagementContext.Employees.Include(e => e.Department)
                        .FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);
                if (employeeToDelete is null)
                {
                    throw new DbUpdateException("Such employee doesn't exist");
                }
                SetDepartmentChangedDateIfExists(employeeToDelete.Department);
                DepartmentManagementContext.Employees.Remove(employeeToDelete);
                await DepartmentManagementContext.SaveChangesAsync();
            }, "Ошибка при удалении сотрудника");
        }

        public async Task<OperationResult> MoveEmployeeToDepartmentAsync(long departmentId, long employeeId)
        {
            Department department =
                DepartmentManagementContext.Departments.FirstOrDefault(d => d.DepartmentId == departmentId);
            if (department is null)
            {
                return OperationResult.Failed("Такого отдела не существует");
            }

            OperationResult<Employee> employeeResult = GetRequiredEmployee(employeeId);
            if (!employeeResult.Success)
            {
                return employeeResult;
            }

            return await EfRepoHelper.InvokeManagingExceptions(async () =>
            {
                SetDepartmentChangedDateIfExists(employeeResult.Result.Department);
                department.WasChangedDate = DateTime.Now;
                employeeResult.Result.Department = department;
                await DepartmentManagementContext.SaveChangesAsync();
            }, "Ошибка при перемещении сотрудника из отдела");
        }

        public async Task<OperationResult> RemoveEmployeeFromDepartmentAsync(long employeeId)
        {
            OperationResult<Employee> employeeResult = GetRequiredEmployee(employeeId);
            if (!employeeResult.Success)
            {
                return employeeResult;
            }

            return await EfRepoHelper.InvokeManagingExceptions(async () =>
            {
                employeeResult.Result.Department.WasChangedDate = DateTime.Now;
                employeeResult.Result.Department = null;
                await DepartmentManagementContext.SaveChangesAsync();
            }, "Ошибка при удалении сотрудника из отдела");
        }

        private OperationResult<Employee> GetRequiredEmployee(long employeeId)
        {
            Employee employee = DepartmentManagementContext.Employees.Include(e => e.Department)
                .FirstOrDefault(e => e.EmployeeId == employeeId);
            if (employee is null)
            {
                return OperationResult<Employee>.Failed("Такого сотрудника не существует");
            }

            return OperationResult<Employee>.Successful(employee);
        }

        private async Task ChangeDepartmentIfNeeded(Employee employeeToEdit, Employee newEmployeeData)
        {
            if (newEmployeeData.Department?.DepartmentId > 0 &&
                employeeToEdit.Department?.DepartmentId != newEmployeeData.Department?.DepartmentId)
            {
                await MoveEmployeeToDepartmentAsync(newEmployeeData.Department.DepartmentId, employeeToEdit.EmployeeId);
            }
        }

        private void SetDepartmentChangedDateIfExists(Department department)
        {
            if (department != null)
            {
                department.WasChangedDate = DateTime.Now;
            }
        }
    }
}
