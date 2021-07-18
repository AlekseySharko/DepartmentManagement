import {AfterViewInit, Component, Input, OnDestroy, ViewChild} from '@angular/core';
import {Employee} from "../../../classes/employee";
import {MatTableDataSource} from "@angular/material/table";
import {MatPaginator} from "@angular/material/paginator";
import {MatSort} from "@angular/material/sort";
import {EmployeeProviderService} from "../../../services/employee-provider.service";
import {Subscription} from "rxjs";
import {DialogMessageHandlerService} from "../../../../../core/services/dialog-message-handler.service";
import {Department} from "../../../classes/department";
import {AreYouSureDialogComponent} from "../../../../../shared/common-dialogs/are-you-sure-dialog/are-you-sure-dialog.component";
import {MatDialog} from "@angular/material/dialog";

@Component({
  selector: 'app-dep-employees-list',
  templateUrl: './dep-employees-list.component.html',
  styleUrls: ['./dep-employees-list.component.css']
})
export class DepEmployeesListComponent implements OnDestroy, AfterViewInit {
  @Input() department: Department = new Department();
  dataSource: MatTableDataSource<Employee> = new MatTableDataSource<Employee>();
  displayedColumns: string[] = ['fullName', 'controls'];
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  employeeRemoveSubscription = new Subscription();
  employeeSubscription = new Subscription();

  constructor(private employeeProvider: EmployeeProviderService,
              private errorHandler: DialogMessageHandlerService,
              private dialog: MatDialog) { }

  ngAfterViewInit(): void {
    this.onChanges();
  }
  ngOnDestroy(): void {
    this.employeeRemoveSubscription.unsubscribe();
    this.employeeSubscription.unsubscribe();
  }

  onChanges() {
    this.dataSource = new MatTableDataSource<Employee>(this.department.employees);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  onDeleteEmployee(employee: Employee) {
    const dialogRef = this.dialog.open(AreYouSureDialogComponent, {
      width: '24rem',
      minWidth: '20rem',
      data: {
        question: `Вы уверены что хотите удалить сотрудника с ФИО: ${employee.fullName} из даного отдела? `,
        boldNextLine: true,
        bold: "Это действие нельзя будет отменить",
        okButton: "Удалить",
        cancelButton: "Отмена"
      }
    });
    dialogRef.afterClosed().subscribe(data => {
        if(data) {
          this.employeeRemoveSubscription = this.employeeProvider.removeFromDepartment(employee.employeeId).subscribe(
            () => {},
            error => {
              this.errorHandler.onHttpError(error);
            },
            () => {
              this.employeeSubscription = this.employeeProvider
                .getEmployees(false, this.department.departmentId).subscribe(data => {
                  this.department.employees = data;
                  this.onChanges();
                });
            }
          );
        }
      }
    )
  }
}
