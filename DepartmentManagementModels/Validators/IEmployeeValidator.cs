using System.Linq;
using DepartmentManagementModels.OperationResults;

namespace DepartmentManagementModels.Validators
{
    public interface IEmployeeValidator
    {
        public OperationResult ValidateOnAdd(Employee employee, IQueryable<Employee> existingEmployees = null);
        public OperationResult ValidateOnEdit(Employee employee, IQueryable<Employee> existingEmployees = null);
        public OperationResult ValidateOnDelete(Employee employee, IQueryable<Employee> existingEmployees = null);
    }
}
