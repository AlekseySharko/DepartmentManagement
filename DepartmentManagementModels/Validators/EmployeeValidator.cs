using System.Linq;
using DepartmentManagementModels.OperationResults;

namespace DepartmentManagementModels.Validators
{
    public class EmployeeValidator : IEmployeeValidator
    {
        public OperationResult ValidateOnAdd(Employee employee, IQueryable<Employee> existingEmployees = null)
        {
            OperationResult result = OperationResult.Successful();

            if (employee is null)
            {
                result.Success = false;
                result.AddToErrorMessage("Сотрудник не может быть null. ");
                return result;
            }
            if (employee.EmployeeId != 0)
            {
                result.Success = false;
                result.AddToErrorMessage("Сотрудник не может иметь id при создании. ");
            }
            if (string.IsNullOrWhiteSpace(employee.FullName))
            {
                result.Success = false;
                result.AddToErrorMessage("ФИО сотрудника не может быть пустым. ");
                return result;
            }
            if (existingEmployees != null &&
                existingEmployees.FirstOrDefault(e => e.FullName.ToLower() == employee.FullName.ToLower()) != null)
            {
                result.Success = false;
                result.AddToErrorMessage("Сотрудник с таким именем уже существует. ");
            }

            return result;
        }

        public OperationResult ValidateOnDelete(Employee employee, IQueryable<Employee> existingEmployees = null)
        {
            OperationResult result = OperationResult.Successful();

            if (employee is null)
            {
                result.Success = false;
                result.AddToErrorMessage("Сотрудник не может быть null. ");
                return result;
            }
            if (existingEmployees != null &&
                existingEmployees.FirstOrDefault(e => e.EmployeeId == employee.EmployeeId) == null)
            {
                result.Success = false;
                result.AddToErrorMessage("Сотрудника с таким id не существует. ");
            }

            return result;
        }

        public OperationResult ValidateOnEdit(Employee employee, IQueryable<Employee> existingEmployees = null)
        {
            OperationResult result = ValidateOnDelete(employee, existingEmployees);

            if (string.IsNullOrWhiteSpace(employee.FullName))
            {
                result.Success = false;
                result.AddToErrorMessage("ФИО сотрудника не может быть пустым. ");
                return result;
            }
            if (existingEmployees != null &&
                existingEmployees.FirstOrDefault(e =>
                    e.FullName.ToLower() == employee.FullName.ToLower() && e.EmployeeId != employee.EmployeeId) != null)
            {
                result.Success = false;
                result.AddToErrorMessage("Сотрудник с таким именем уже существует. ");
            }

            return result;
        }
    }
}
