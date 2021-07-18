using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DepartmentManagementModels.Repositories
{
    public interface IEmployeeRepository
    {
        IQueryable<Employee> GetEmployees(bool includeDepartment = false, Expression<Func<Employee, bool>> whereExpression = null);
        Task<OperationResults.OperationResult> AddEmployeeAsync(Employee employee);
        Task<OperationResults.OperationResult> EditEmployeeAsync(Employee employee);
        Task<OperationResults.OperationResult> DeleteEmployeeAsync(Employee employee);
        Task<OperationResults.OperationResult> MoveEmployeeToDepartmentAsync(long departmentId, long employeeId);
        Task<OperationResults.OperationResult> RemoveEmployeeFromDepartmentAsync(long employeeId);
    }
}
