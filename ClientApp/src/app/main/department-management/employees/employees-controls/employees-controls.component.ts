import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Employee} from "../../classes/employee";
import {FormEmployeeData, FormEmployeeDialogComponent} from "../form-employee-dialog/form-employee-dialog.component";
import {MatDialog} from "@angular/material/dialog";
import {AreYouSureDialogComponent} from "../../../../shared/common-dialogs/are-you-sure-dialog/are-you-sure-dialog.component";
import {EmployeeProviderService} from "../../services/employee-provider.service";
import {DialogMessageHandlerService} from "../../../../core/services/dialog-message-handler.service";

@Component({
  selector: 'app-employees-controls',
  templateUrl: './employees-controls.component.html',
  styleUrls: ['./employees-controls.component.css']
})
export class EmployeesControlsComponent implements OnInit {
  @Input() employee: Employee = new Employee();
  @Output() employeesChanged: EventEmitter<any> = new EventEmitter<any>();
  formEmployeeData: FormEmployeeData = new FormEmployeeData();

  constructor(private dialog: MatDialog,
              private employeeProvider: EmployeeProviderService,
              private errorHandler: DialogMessageHandlerService) { }

  ngOnInit(): void {
    this.formEmployeeData.isEdit = true;
    this.formEmployeeData.employee = this.employee;
  }

  onEdit() {
    const dialogRef = this.dialog.open(FormEmployeeDialogComponent, {
      width: '40rem',
      minWidth: '20rem',
      data: { isEdit: true, employee: this.employee }
    });
    dialogRef.afterClosed().subscribe(data => {
      if(data) {
        this.employeesChanged.emit();
      }
    })
  }
  onDelete() {
    const dialogRef = this.dialog.open(AreYouSureDialogComponent, {
      width: '24rem',
      minWidth: '20rem',
      data: {
        question: "Вы уверены что хотите удалить сотрудника с ФИО: ",
        bold: this.employee.fullName,
        okButton: "Удалить",
        cancelButton: "Отмена"
      }
    });
    dialogRef.afterClosed().subscribe(data => {
        if(data) {
          this.employeeProvider.deleteEmployee(this.employee.employeeId).subscribe(
            () => {},
            error => this.errorHandler.onHttpError(error),
            () => this.employeesChanged.emit()
          )
        }
      }
    )
  }
}
