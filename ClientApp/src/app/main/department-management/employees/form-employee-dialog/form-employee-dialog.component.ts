import {Component, Inject, OnDestroy, OnInit} from '@angular/core';
import {Employee} from "../../classes/employee";
import {Observable, Subject, Subscription} from "rxjs";
import {EmployeeProviderService} from "../../services/employee-provider.service";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {Department} from "../../classes/department";
import {DialogMessageHandlerService} from "../../../../core/services/dialog-message-handler.service";

export class FormEmployeeData {
  isEdit: boolean = false;
  employee: Employee | null = null;
}

@Component({
  selector: 'app-form-employee-dialog',
  templateUrl: './form-employee-dialog.component.html',
  styleUrls: ['./form-employee-dialog.component.css']
})
export class FormEmployeeDialogComponent implements OnInit, OnDestroy {
  localEmployee: Employee = new Employee();
  departmentIdChanged = new Subject<number>();
  submitSubscription = new Subscription();
  constructor(private dialogRef: MatDialogRef<FormEmployeeDialogComponent>,
              private employeeProvider: EmployeeProviderService,
              private errorHandler: DialogMessageHandlerService,
              @Inject(MAT_DIALOG_DATA) public data: FormEmployeeData) {}

  ngOnInit(): void {
    if(this.data.isEdit) {
      this.localEmployee = new Employee(this.data.employee);
      this.onDepartmentChanged(this.localEmployee?.department ?? new Department());
    }
  }
  ngOnDestroy(): void {
    this.submitSubscription.unsubscribe();
  }
  onCancel() {
    this.dialogRef.close();
  }
  getInitialDepartment() {
    return this.localEmployee?.department?.departmentId ?? 0;
  }
  onDepartmentChanged(department: Department) {
    this.departmentIdChanged.next(department.departmentId);
    this.localEmployee.department = department;
  }
  checkValidity() {
    return !!this.localEmployee.department &&
      !!this.localEmployee.fullName &&
      !!this.localEmployee.position &&
      !!this.localEmployee.wasEmployedDate
  }
  onSubmit() {
    let observable: Observable<Object>;
    if(this.data.isEdit) {
      observable = this.employeeProvider.putEmployee(this.localEmployee);
    } else {
      observable =this.employeeProvider.postEmployee(this.localEmployee);
    }
    this.submitSubscription = observable.subscribe(
      () => {},
      error => {
        this.errorHandler.onHttpError(error);
        this.dialogRef.close();
      },
      () => {
        this.dialogRef.close(true);
      }
    )
  }
}
