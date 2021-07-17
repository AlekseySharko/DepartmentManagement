import {Component, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {DepartmentProviderService} from "../services/department-provider.service";
import {Department} from "../classes/department";
import {Subscription} from "rxjs";
import {MatPaginator} from "@angular/material/paginator";
import {MatSort} from "@angular/material/sort";
import {MatTableDataSource} from "@angular/material/table";
import {Employee} from "../classes/employee";

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

  constructor(private departmentProvider: DepartmentProviderService) { }

  ngOnInit(): void {
    this.departmentSubscription = this.departmentProvider.getDepartments(true).subscribe(data => {
      this.departments = data;
      this.dataSource = new MatTableDataSource<Department>(this.departments);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }
  ngOnDestroy(): void {
    this.departmentSubscription.unsubscribe();
  }
}
