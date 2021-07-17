import {AfterViewInit, Component, Input, OnDestroy, ViewChild} from '@angular/core';
import {Employee} from "../../../classes/employee";
import {MatTableDataSource} from "@angular/material/table";
import {MatPaginator} from "@angular/material/paginator";
import {MatSort} from "@angular/material/sort";
import {EmployeeProviderService} from "../../../services/employee-provider.service";
import {Subscription} from "rxjs";
import {DialogMessageHandlerService} from "../../../../../core/services/dialog-message-handler.service";
import {Department} from "../../../classes/department";

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
              private errorHandler: DialogMessageHandlerService) { }

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
