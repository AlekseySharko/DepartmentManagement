using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DepartmentManagementModels.Repositories
{
    public interface IDepartmentRepository
    {
        IQueryable<Department> GetDepartments(bool includeEmployees = false, Expression<Func<Department, bool>> whereExpression = null);
        IEnumerable<string> GetExistingPositions(long departmentId);
        Task<OperationResults.OperationResult> AddDepartmentAsync(Department department);
        Task<OperationResults.OperationResult> EditDepartmentAsync(Department department);
        Task<OperationResults.OperationResult> DeleteDepartmentAsync(Department department);
    }
}
