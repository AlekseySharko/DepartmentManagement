using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DepartmentManagementModels;
using DepartmentManagementModels.OperationResult;
using DepartmentManagementModels.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentManagementAsp.Controllers
{
    [Route("api/departments")]
    public class DepartmentController : Controller
    {
        private IDepartmentRepository DepartmentRepository { get; }

        public DepartmentController(IDepartmentRepository departmentRepository) =>
            DepartmentRepository = departmentRepository;

        [HttpGet]
        public IActionResult GetDepartments([FromQuery] bool includeEmployees)
        {
            return Ok(BreakDepartmentReferenceCycle(DepartmentRepository.GetDepartments(includeEmployees)));
        }

        [HttpPost]
        public async Task<IActionResult> PostDepartment([FromBody] Department department)
        {
            OperationResult operationResult = await DepartmentRepository.AddDepartmentAsync(department);
            return GetResultFromOperationResult(operationResult);
        }

        [HttpPut]
        public async Task<IActionResult> PutDepartment([FromBody] Department department)
        {
            OperationResult operationResult = await DepartmentRepository.EditDepartmentAsync(department);
            return GetResultFromOperationResult(operationResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> PutDepartment([FromRoute] long id)
        {
            OperationResult operationResult =
                await DepartmentRepository.DeleteDepartmentAsync(new Department {DepartmentId = id});
            return GetResultFromOperationResult(operationResult);
        }

        [HttpGet("positions")]
        public IActionResult GetExistingPositions([FromQuery] long departmentId)
        {
            return Ok(DepartmentRepository.GetExistingPositions(departmentId));
        }

        private IActionResult GetResultFromOperationResult(OperationResult operationResult)
        {
            if (operationResult.Success)
            {
                return Ok();
            }

            return BadRequest(operationResult.Message);
        }
        private IEnumerable<Department> BreakDepartmentReferenceCycle(IQueryable<Department> departments)
        {
            var listDepartments = departments.ToList();
            foreach (Department department in listDepartments)
            {
                if (department?.Employees is null || department.Employees.Count < 1)
                {
                    break;
                }
                foreach (Employee departmentEmployee in department.Employees)
                {
                    if (departmentEmployee.Department != null)
                    {
                        departmentEmployee.Department = null;
                    }
                }
            }

            return listDepartments;
        }
    }
}
