using System.Linq;
using DepartmentManagementModels.OperationResults;

namespace DepartmentManagementModels.Validators
{
    public class DepartmentValidator : IDepartmentValidator
    {
        public OperationResult ValidateOnAdd(Department department, IQueryable<Department> existingDepartments = null)
        {
            OperationResult result = OperationResult.Successful();

            if (department is null)
            {
                result.Success = false;
                result.AddToErrorMessage("Отдел не может быть null. ");
                return result;
            }
            if (department.DepartmentId != 0)
            {
                result.Success = false;
                result.AddToErrorMessage("Отдел не может иметь id при создании. ");
            }
            if (string.IsNullOrWhiteSpace(department.Name))
            {
                result.Success = false;
                result.AddToErrorMessage("Имя отдела не может быть пустым. ");
                return result;
            }
            if (existingDepartments != null &&
                existingDepartments.FirstOrDefault(d => d.Name.ToLower() == department.Name.ToLower()) != null)
            {
                result.Success = false;
                result.AddToErrorMessage("Отдел с таким именем уже существует. ");
            }

            return result;
        }

        public OperationResult ValidateOnDelete(Department department, IQueryable<Department> existingDepartments = null)
        {
            OperationResult result = OperationResult.Successful();

            if (department is null)
            {
                result.Success = false;
                result.AddToErrorMessage("Отдел не может быть null. ");
                return result;
            }
            if (existingDepartments != null && existingDepartments.FirstOrDefault(d => d.DepartmentId == department.DepartmentId) == null)
            {
                result.Success = false;
                result.AddToErrorMessage("Отдела с таким id не существует. ");
            }

            return result;
        }

        public OperationResult ValidateOnEdit(Department department, IQueryable<Department> existingDepartments = null)
        {
            OperationResult result = ValidateOnDelete(department, existingDepartments);

            if (string.IsNullOrWhiteSpace(department.Name))
            {
                result.Success = false;
                result.AddToErrorMessage("Имя отдела не может быть пустым. ");
                return result;
            }
            if (existingDepartments != null &&
                existingDepartments.FirstOrDefault(d =>
                    d.Name.ToLower() == department.Name.ToLower() && d.DepartmentId != department.DepartmentId) != null)
            {
                result.Success = false;
                result.AddToErrorMessage("Отдел с таким именем уже существует. ");
            }

            return result;
        }
    }
}
