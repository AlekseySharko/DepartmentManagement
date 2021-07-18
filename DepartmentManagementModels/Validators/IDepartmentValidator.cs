using System.Linq;
using DepartmentManagementModels.OperationResults;

namespace DepartmentManagementModels.Validators
{
    public interface IDepartmentValidator
    {
        public OperationResult ValidateOnAdd(Department department, IQueryable<Department> existingDepartments = null);
        public OperationResult ValidateOnEdit(Department department, IQueryable<Department> existingDepartments = null);
        public OperationResult ValidateOnDelete(Department department, IQueryable<Department> existingDepartments = null);
    }
}
