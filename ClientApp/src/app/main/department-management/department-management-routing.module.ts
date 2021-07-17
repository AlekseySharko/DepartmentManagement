import {NgModule} from "@angular/core";
import {Route, RouterModule} from "@angular/router";
import {DepartmentsComponent} from "./departments/departments.component";
import {EmployeesComponent} from "./employees/employees.component";
import {DepartmentManagementComponent} from "./department-management.component";

const departmentManagementRoutes : Route[] = [
  { path: "departments", component: DepartmentsComponent },
  { path: "employees", component: EmployeesComponent }
]
const routes: Route[] = [
  {path: "", component: DepartmentManagementComponent, children: departmentManagementRoutes}
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DepartmentManagementRoutingModule {

}
