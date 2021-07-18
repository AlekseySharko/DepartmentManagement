import {Component, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {Department} from "../../classes/department";
import {DepartmentProviderService} from "../../services/department-provider.service";
import {AreYouSureDialogComponent} from "../../../../shared/common-dialogs/are-you-sure-dialog/are-you-sure-dialog.component";
import {MatDialog} from "@angular/material/dialog";
import {DialogMessageHandlerService} from "../../../../core/services/dialog-message-handler.service";
import {FormDepartmentDialogComponent} from "../form-department-dialog/form-department-dialog.component";

@Component({
  selector: 'app-department-controls',
  templateUrl: './department-controls.component.html',
  styleUrls: ['./department-controls.component.css']
})
export class DepartmentControlsComponent implements OnInit {
  @Input() department: Department = new Department();
  @Output() departmentChanged: EventEmitter<any> = new EventEmitter<any>();

  constructor(private departmentProvider: DepartmentProviderService,
              private dialog: MatDialog,
              private errorHandler: DialogMessageHandlerService) { }

  ngOnInit(): void {}

  onEdit() {
    const dialogRef = this.dialog.open(FormDepartmentDialogComponent, {
      width: '40rem',
      minWidth: '20rem',
      data: { isEdit: true, department: this.department }
    });
    dialogRef.afterClosed().subscribe(data => {
        this.departmentChanged.emit();
    });
  }
  onDelete() {
    const dialogRef = this.dialog.open(AreYouSureDialogComponent, {
      width: '24rem',
      minWidth: '20rem',
      data: {
        question: "Вы уверены, что хотите удалить ",
        bold: this.department.name + '?',
        okButton: "Удалить",
        cancelButton: "Отмена"
      }
    });
    dialogRef.afterClosed().subscribe(data => {
        if(data) {
          this.departmentProvider.deleteDepartment(this.department.departmentId).subscribe(
            () => {},
            error => this.errorHandler.onHttpError(error),
            () => this.departmentChanged.emit()
          )
        }
      }
    )
  }

}
