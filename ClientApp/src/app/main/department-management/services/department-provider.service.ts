import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Department} from "../classes/department";
import {map} from "rxjs/operators";

@Injectable()
export class DepartmentProviderService {
  constructor(private http: HttpClient) {}

  getDepartments(includeEmployees: boolean = false) {
      return this.http.get<Department[]>("api/departments", {
        params: new HttpParams().set("includeEmployees", includeEmployees)
      }).pipe(
          map(departments => {
            this.transformToDates(departments);
            return departments;
          })
      );
  }
  postDepartment(department: Department) {
    department.wasAddedDate = new Date();
    return this.http.post("api/departments", department);
  }
  putDepartment(department: Department) {
    department.wasAddedDate = new Date();
    return this.http.put("api/departments", department);
  }
  deleteDepartment(departmentId: number) {
    return this.http.delete("api/departments/" + departmentId)
  }
  getExistingPositions(departmentId: number) {
    return this.http.get<string[]>("api/departments/positions", {
      params: new HttpParams().set("departmentId", departmentId)
    }).pipe(
      map(positions => {
        return positions?.sort((a, b) => a.toLowerCase().localeCompare(b.toLowerCase()));
      })
    )
  }

  private transformToDates(departments: Department[]) {
    departments.forEach(department => {
      department.wasChangedDate = new Date(department.wasChangedDate?.toString() ?? "");
      if(isNaN(department.wasChangedDate.getTime())) {
        department.wasChangedDate = null;
      }
      department.wasAddedDate = new Date(department.wasAddedDate?.toString() ?? "");
      if(isNaN(department.wasAddedDate.getTime())) {
        department.wasAddedDate = null;
      }
    });
  }
}
