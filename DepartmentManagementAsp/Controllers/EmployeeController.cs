using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DepartmentManagementModels;
using DepartmentManagementModels.OperationResults;
using DepartmentManagementModels.Repositories;
using DepartmentManagementModels.Validators;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentManagementAsp.Controllers
{
    [Route("api/employees")]
    public class EmployeeController : Controller
    {
        private IEmployeeRepository EmployeeRepository { get; }
        private IEmployeeValidator EmployeeValidator { get; }

        public EmployeeController(IEmployeeRepository employeeRepository, IEmployeeValidator employeeValidator)
        {
            EmployeeRepository = employeeRepository;
            EmployeeValidator = employeeValidator;
        }

        [HttpGet]
        public IActionResult GetEmployees([FromQuery] long departmentId = 0, bool includeDepartment = false)
        {
            IQueryable<Employee> employees = departmentId < 1
                ? EmployeeRepository.GetEmployees(includeDepartment)
                : EmployeeRepository.GetEmployees(includeDepartment, e => e.Department.DepartmentId == departmentId);
            
            return Ok(BreakDepReferenceCycle(employees));
        }

        [HttpPost]
        public async Task<IActionResult> PostEmployee([FromBody] Employee employee)
        {
            if (ValidateEmployeeOrBadRequest(employee, EmployeeValidator.ValidateOnAdd) is BadRequestObjectResult badRequest)
            {
                return badRequest;
            }

            OperationResult operationResult = await EmployeeRepository.AddEmployeeAsync(employee);
            return GetResultFromOperationResult(operationResult);
        }

        [HttpPut]
        public async Task<IActionResult> PutEmployee([FromBody] Employee employee)
        {
            if (ValidateEmployeeOrBadRequest(employee, EmployeeValidator.ValidateOnEdit) is BadRequestObjectResult badRequest)
            {
                return badRequest;
            }

            OperationResult operationResult = await EmployeeRepository.EditEmployeeAsync(employee);
            return GetResultFromOperationResult(operationResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] long id)
        {
            Employee employee = new Employee { EmployeeId = id };
            if (ValidateEmployeeOrBadRequest(employee, EmployeeValidator.ValidateOnDelete) is BadRequestObjectResult badRequest)
            {
                return badRequest;
            }

            OperationResult operationResult =
                await EmployeeRepository.DeleteEmployeeAsync(employee);
            return GetResultFromOperationResult(operationResult);
        }

        [HttpDelete("{employeeId}/from-department")]
        public async Task<IActionResult> RemoveFromDepartment([FromRoute] long employeeId)
        {
            Employee employee = new Employee { EmployeeId = employeeId };
            if (ValidateEmployeeOrBadRequest(employee, EmployeeValidator.ValidateOnDelete) is BadRequestObjectResult badRequest)
            {
                return badRequest;
            }
            OperationResult operationResult = await EmployeeRepository.RemoveEmployeeFromDepartmentAsync(employeeId);
            return GetResultFromOperationResult(operationResult);
        }

        private IActionResult GetResultFromOperationResult(OperationResult operationResult)
        {
            if (operationResult.Success)
            {
                return Ok();
            }

            return BadRequest(operationResult.Message);
        }

        private IActionResult ValidateEmployeeOrBadRequest(Employee employee, Func<Employee, IQueryable<Employee>, OperationResult> validator)
        {
            OperationResult validationResult = validator(employee, EmployeeRepository.GetEmployees());
            if (!validationResult.Success)
            {
                return BadRequest(validationResult.Message);
            }

            return null;
        }

        private IEnumerable<Employee> BreakDepReferenceCycle(IQueryable<Employee> employees)
        {
            var listEmployees = employees.ToList();
            foreach (var employee in listEmployees)
            {
                if (employee?.Department?.Employees != null)
                {
                    employee.Department.Employees = null;
                }
            }

            return listEmployees;
        }
    }
}
