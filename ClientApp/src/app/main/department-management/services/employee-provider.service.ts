import { Injectable } from '@angular/core';
import {map} from "rxjs/operators";
import {HttpClient, HttpParams} from "@angular/common/http";
import {Employee} from "../classes/employee";

@Injectable()
export class EmployeeProviderService {
  constructor(private http: HttpClient) {}

  getEmployees(includeDepartment: boolean = false) {
    return this.http.get<Employee[]>("api/employees", {
      params: new HttpParams().set("includeDepartment", includeDepartment)
    }).pipe(
        map(employees => {
          this.transformToDates(employees);
          return employees;
        })
      );
  }
  postEmployee(employee: Employee) {
    employee.wasAddedDate = new Date();
    return this.http.post("api/employees", employee);
  }
  putEmployee(employee: Employee) {
    employee.wasAddedDate = new Date();
    return this.http.put("api/employees", employee);
  }
  deleteEmployee(employeeId: number) {
    return this.http.delete("api/employees/" + employeeId);
  }
  private transformToDates(employees: Employee[]) {
    employees.forEach(employee => {
      employee.wasChangedDate = new Date(employee.wasChangedDate?.toString() ?? "");
      if(isNaN(employee.wasChangedDate.getTime())) {
        employee.wasChangedDate = null;
      }
      employee.wasAddedDate = new Date(employee.wasAddedDate?.toString() ?? "");
      if(isNaN(employee.wasAddedDate.getTime())) {
        employee.wasAddedDate = null;
      }
      employee.wasEmployedDate = new Date(employee.wasEmployedDate?.toString() ?? "");
      if(isNaN(employee.wasEmployedDate.getTime())) {
        employee.wasEmployedDate = null;
      }
    });
  }
}
