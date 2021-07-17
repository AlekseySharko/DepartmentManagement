import {Component, EventEmitter, Input, OnDestroy, OnInit, Output} from '@angular/core';
import {Department} from "../../classes/department";
import {DepartmentProviderService} from "../../services/department-provider.service";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-department-select',
  templateUrl: './department-select.component.html',
  styleUrls: ['./department-select.component.css']
})
export class DepartmentSelectComponent implements OnInit, OnDestroy {
  @Input() initialDepartmentId: number = 0;
  @Output() departmentChanged: EventEmitter<Department> = new EventEmitter<Department>();
  departments: Department[] = [];
  selectedDepartment: Department = new Department();
  departmentSubscription: Subscription = new Subscription();

  constructor(private departmentProvider: DepartmentProviderService) { }

  ngOnInit(): void {
    this.departmentSubscription = this.departmentProvider.getDepartments().subscribe(data => {
      this.departments = data;
      this.selectedDepartment = this.departments.find(d => d.departmentId === this.initialDepartmentId) ?? new Department();
    })
  }
  ngOnDestroy(): void {
    this.departmentSubscription.unsubscribe();
  }
  onDepartmentChange(){
    this.departmentChanged.emit(this.selectedDepartment);
  }
}
