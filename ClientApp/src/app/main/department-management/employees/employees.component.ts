import {Component, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {Employee} from "../classes/employee";
import {EmployeeProviderService} from "../services/employee-provider.service";
import {Subscription} from "rxjs";
import {MatTableDataSource} from "@angular/material/table";
import {MatPaginator} from "@angular/material/paginator";
import {MatSort} from "@angular/material/sort";
import {Department} from "../classes/department";
import {FormEmployeeDialogComponent} from "./form-employee-dialog/form-employee-dialog.component";
import {MatDialog} from "@angular/material/dialog";
import {DialogMessageHandlerService} from "../../../core/services/dialog-message-handler.service";

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent implements OnInit, OnDestroy {
  employees: Employee[] = [];
  employeesSubscription: Subscription = new Subscription();
  dataSource!: MatTableDataSource<Employee>;
  displayedColumns: string[] = ['fullName', 'department', 'position', 'wasAddedDate', 'wasChangedDate', 'wasEmployedDate', 'controls'];
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  displayAddEmployee = false;
  dialogSubscription = new Subscription();

  constructor(private employeeProvider: EmployeeProviderService,
              private dialog: MatDialog) { }
  ngOnInit(): void {
    this.onChange();
  }
  ngOnDestroy(): void {
    this.employeesSubscription.unsubscribe();
    this.dialogSubscription.unsubscribe()
  }
  getRealDepartments(employees: Employee[]) {
    employees.forEach(employee => {
      employee.department = new Department(employee.department);
    })
  }
  onAddEmployee() {
    const dialogRef = this.dialog.open(FormEmployeeDialogComponent, {
      width: '40rem',
      minWidth: '20rem',
      data: { isEdit: false }
    });
    this.dialogSubscription = dialogRef.afterClosed().subscribe(data => {
      if(data) {
        this.onChange();
      }
    });
  }
  onChange(){
    this.employeesSubscription = this.employeeProvider.getEmployees(true).subscribe(data => {
      this.employees = data;
      this.getRealDepartments(this.employees);
      this.dataSource = new MatTableDataSource<Employee>(this.employees);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    })
  }
}
