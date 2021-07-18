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
    [Route("api/departments")]
    public class DepartmentController : Controller
    {
        private IDepartmentRepository DepartmentRepository { get; }
        private IDepartmentValidator DepartmentValidator { get; }

        public DepartmentController(IDepartmentRepository departmentRepository, IDepartmentValidator departmentValidator)
        {
            DepartmentRepository = departmentRepository;
            DepartmentValidator = departmentValidator;
        }

        [HttpGet]
        public IActionResult GetDepartments([FromQuery] bool includeEmployees)
        {
            return Ok(BreakDepartmentReferenceCycle(DepartmentRepository.GetDepartments(includeEmployees)));
        }

        [HttpPost]
        public async Task<IActionResult> PostDepartment([FromBody] Department department)
        {
            if (ValidateDepartmentOrBadRequest(department, DepartmentValidator.ValidateOnAdd) is BadRequestObjectResult badRequest)
            {
                return badRequest;
            }
            OperationResult repositoryResult = await DepartmentRepository.AddDepartmentAsync(department);
            return GetResultFromRepositoryResult(repositoryResult);
        }

        [HttpPut]
        public async Task<IActionResult> PutDepartment([FromBody] Department department)
        {
            if (ValidateDepartmentOrBadRequest(department, DepartmentValidator.ValidateOnEdit) is BadRequestObjectResult badRequest)
            {
                return badRequest;
            }
            OperationResult repositoryResult = await DepartmentRepository.EditDepartmentAsync(department);
            return GetResultFromRepositoryResult(repositoryResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> PutDepartment([FromRoute] long id)
        {
            Department department = new Department { DepartmentId = id };
            if (ValidateDepartmentOrBadRequest(department, DepartmentValidator.ValidateOnDelete) is BadRequestObjectResult badRequest)
            {
                return badRequest;
            }
            OperationResult repositoryResult = await DepartmentRepository.DeleteDepartmentAsync(department);
            return GetResultFromRepositoryResult(repositoryResult);
        }

        [HttpGet("positions")]
        public IActionResult GetExistingPositions([FromQuery] long departmentId)
        {
            return Ok(DepartmentRepository.GetExistingPositions(departmentId));
        }

        private IActionResult GetResultFromRepositoryResult(OperationResult repositoryResult)
        {
            if (repositoryResult.Success)
            {
                return Ok();
            }

            return BadRequest(repositoryResult.Message);
        }

        private IActionResult ValidateDepartmentOrBadRequest(Department department, Func<Department, IQueryable<Department>, OperationResult> validator)
        {
            OperationResult validationResult = validator(department, DepartmentRepository.GetDepartments());
            if (!validationResult.Success)
            {
                return BadRequest(validationResult.Message);
            }

            return null;
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
