import {Employee} from "./employee";

export class Department {
  departmentId: number = 0;
  name: string = '';
  wasAddedDate: Date | null = null;
  wasChangedDate: Date | null = null;
  employees: Employee[] = [];

  toString(): string {
    return this.name;
  }

  constructor(department: Department|null = null) {
    if(!department) return this;
    this.departmentId = department.departmentId;
    this.name = department.name;
    this.wasChangedDate = department.wasChangedDate;
    this.employees = department.employees;
    this.wasAddedDate = department.wasAddedDate;
  }
}
