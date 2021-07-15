using System;

namespace DepartmentManagementModels
{
    public class Employee
    {
        public long EmployeeId { get; set; }
        public string FullName { get; set; }
        public DateTime WasAddedDate { get; set; }
        public DateTime? WasChangedDate { get; set; }
        public DateTime? WasEmployedDate { get; set; }
        public string Position { get; set; }
        public Department Department { get; set; }
    }
}
