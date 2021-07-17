using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DepartmentManagementModels.Repositories
{
    public interface IEmployeeRepository
    {
        IQueryable<Employee> GetEmployees(bool includeDepartment = false, Expression<Func<Employee, bool>> whereExpression = null);
        Task<OperationResult.OperationResult> AddEmployeeAsync(Employee employee);
        Task<OperationResult.OperationResult> EditEmployeeAsync(Employee employee);
        Task<OperationResult.OperationResult> DeleteEmployeeAsync(Employee employee);
        Task<OperationResult.OperationResult> MoveEmployeeToDepartmentAsync(long departmentId, long employeeId);
        Task<OperationResult.OperationResult> RemoveEmployeeFromDepartmentAsync(long employeeId);
    }
}
