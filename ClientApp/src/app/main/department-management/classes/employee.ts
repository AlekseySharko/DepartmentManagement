import {Department} from "./department";

export class Employee {
  employeeId: number = 0;
  fullName: string = '';
  wasAddedDate: Date | null = null;
  wasChangedDate: Date | null = null;
  wasEmployedDate: Date | null = null;
  position: string = '';
  department: Department | null = null;

  constructor(employee: Employee | null = null) {
    if(!employee) return this;
    this.employeeId = employee.employeeId;
    this.fullName = employee.fullName;
    this.wasAddedDate = employee.wasAddedDate;
    this.wasChangedDate = employee.wasChangedDate;
    this.wasEmployedDate = employee.wasEmployedDate;
    this.position = employee.position;
    this.department = employee.department;
  }

}
