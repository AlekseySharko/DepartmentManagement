import {Component, Inject, OnDestroy, OnInit, Output} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {DialogMessageHandlerService} from "../../../../core/services/dialog-message-handler.service";
import {Department} from "../../classes/department";
import {DepartmentProviderService} from "../../services/department-provider.service";
import {Subscription} from "rxjs";

export class FormDepartmentData {
  isEdit: boolean = false;
  department: Department = new Department();
}

@Component({
  selector: 'app-form-departments-dialog',
  templateUrl: './form-department-dialog.component.html',
  styleUrls: ['./form-department-dialog.component.css']
})
export class FormDepartmentDialogComponent implements OnInit, OnDestroy {
  localDepartment = new Department();
  departmentSubscription = new Subscription();

  constructor(private dialogRef: MatDialogRef<FormDepartmentDialogComponent>,
              private departmentProvider: DepartmentProviderService,
              private errorHandler: DialogMessageHandlerService,
              @Inject(MAT_DIALOG_DATA) public data: FormDepartmentData) {}

  ngOnInit(): void {
    this.localDepartment = new Department(this.data.department);
  }
  ngOnDestroy(): void {
    this.departmentSubscription.unsubscribe();
  }
  onSubmit() {
    let observable;
    if(this.data.isEdit) {
      observable = this.departmentProvider.putDepartment(this.localDepartment);
    } else {
      observable = this.departmentProvider.postDepartment(this.localDepartment);
    }
    this.departmentSubscription = observable.subscribe(
      () => {},
      error => {
        this.errorHandler.onHttpError(error);
        this.dialogRef.close();
      },
      () => this.dialogRef.close(true)
    )
  }
  onCancel() {
    this.dialogRef.close();
  }
  checkValidity() {
    return true;
  }
}
