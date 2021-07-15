using System;
using System.Collections.Generic;

namespace DepartmentsAndEmployeesModels
{
    public class Department
    {
        public long DepartmentId { get; set; }
        public string Name { get; set; }
        public DateTime WasAddedDate { get; set; }
        public DateTime? WasChangedDate { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
