import {NgModule} from "@angular/core";
import {Route, RouterModule} from "@angular/router";

const mainRoutes: Route[] = [
  {path: "management", loadChildren: () => import("./department-management/department-management.module").then(m => m.DepartmentManagementModule)}
]

@NgModule({
  imports: [RouterModule.forChild(mainRoutes)],
  exports: [RouterModule]
})
export class MainRoutingModule {}
