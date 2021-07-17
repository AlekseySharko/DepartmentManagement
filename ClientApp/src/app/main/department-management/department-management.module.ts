import { NgModule } from '@angular/core';
import {CommonModule} from '@angular/common';
import {DepartmentManagementRoutingModule} from "./department-management-routing.module";
import {DepartmentsComponent} from './departments/departments.component';
import {EmployeesComponent} from './employees/employees.component';
import {DepartmentProviderService} from "./services/department-provider.service";
import {DepartmentManagementComponent} from './department-management.component';
import {MatTableModule} from "@angular/material/table";
import {MatPaginatorModule} from "@angular/material/paginator";
import { DepartmentControlsComponent } from './departments/department-controls/department-controls.component';
import { ButtonControlGroupComponent } from './shared/button-control-group/button-control-group.component';
import { EmployeesControlsComponent } from './employees/employees-controls/employees-controls.component';
import {EmployeeProviderService} from "./services/employee-provider.service";
import {MatSortModule} from "@angular/material/sort";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {CommonDialogsModule} from "../../shared/common-dialogs/common-dialogs.module";
import {FormsModule} from "@angular/forms";
import {MatDatepickerModule} from "@angular/material/datepicker";
import {MatNativeDateModule} from "@angular/material/core";
import { DepartmentSelectComponent } from './shared/department-select/department-select.component';
import {MatSelectModule} from "@angular/material/select";
import { PositionInputComponent } from './employees/form-employee-dialog/position-input/position-input.component';
import {MatAutocompleteModule} from "@angular/material/autocomplete";
import { FormEmployeeDialogComponent } from './employees/form-employee-dialog/form-employee-dialog.component';
import {MatDialog, MatDialogModule} from "@angular/material/dialog";



@NgModule({
  declarations: [
    DepartmentsComponent,
    EmployeesComponent,
    DepartmentManagementComponent,
    DepartmentControlsComponent,
    ButtonControlGroupComponent,
    EmployeesControlsComponent,
    DepartmentSelectComponent,
    PositionInputComponent,
    FormEmployeeDialogComponent
  ],
  imports: [
    CommonModule,
    DepartmentManagementRoutingModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatFormFieldModule,
    MatInputModule,
    CommonDialogsModule,
    FormsModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule,
    MatAutocompleteModule,
    MatDialogModule
  ],
  providers: [
    DepartmentProviderService,
    EmployeeProviderService
  ]
})
export class DepartmentManagementModule { }
