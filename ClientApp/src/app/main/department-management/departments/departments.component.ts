import {Component, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {DepartmentProviderService} from "../services/department-provider.service";
import {Department} from "../classes/department";
import {Subscription} from "rxjs";
import {MatPaginator} from "@angular/material/paginator";
import {MatSort} from "@angular/material/sort";
import {MatTableDataSource} from "@angular/material/table";
import {FormDepartmentDialogComponent} from "./form-department-dialog/form-department-dialog.component";
import {MatDialog} from "@angular/material/dialog";

@Component({
  selector: 'app-departments',
  templateUrl: './departments.component.html',
  styleUrls: ['./departments.component.css']
})
export class DepartmentsComponent implements OnInit, OnDestroy {
  departments: Department[] = [];
  departmentSubscription: Subscription = new Subscription();
  displayedColumns: string[] = ['name', 'wasAddedDate', 'wasChangedDate', 'controls'];
  dataSource!: MatTableDataSource<Department>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private departmentProvider: DepartmentProviderService,
              private dialog: MatDialog) { }

  ngOnInit(): void {
    this.onChange();
  }
  ngOnDestroy(): void {
    this.departmentSubscription.unsubscribe();
  }
  onAddDepartment() {
    const dialogRef = this.dialog.open(FormDepartmentDialogComponent, {
      width: '40rem',
      minWidth: '20rem',
      data: { isEdit: false }
    });
    dialogRef.afterClosed().subscribe(data => {
      if(data) {
        this.onChange();
      }
    });
  }
  onChange() {
    this.departmentSubscription = this.departmentProvider.getDepartments(true).subscribe(data => {
      this.departments = data;
      this.dataSource = new MatTableDataSource<Department>(this.departments);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }
}
