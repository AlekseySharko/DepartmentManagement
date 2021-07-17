using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DepartmentManagementModels;
using DepartmentManagementModels.OperationResult;
using DepartmentManagementModels.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentManagementAsp.Controllers
{
    [Route("api/employees")]
    public class EmployeeController : Controller
    {
        private IEmployeeRepository EmployeeRepository { get; }
        public EmployeeController(IEmployeeRepository employeeRepository) => EmployeeRepository = employeeRepository;

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
            OperationResult operationResult = await EmployeeRepository.AddEmployeeAsync(employee);
            return GetResultFromOperationResult(operationResult);
        }

        [HttpPut]
        public async Task<IActionResult> PutEmployee([FromBody] Employee employee)
        {
            OperationResult operationResult = await EmployeeRepository.EditEmployeeAsync(employee);
            return GetResultFromOperationResult(operationResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] long id)
        {
            OperationResult operationResult =
                await EmployeeRepository.DeleteEmployeeAsync(new Employee {EmployeeId = id});
            return GetResultFromOperationResult(operationResult);
        }

        [HttpDelete("{employeeId}/from-department")]
        public async Task<IActionResult> RemoveFromDepartment([FromRoute] long employeeId)
        {
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
