import {Component, Input, OnInit} from '@angular/core';
import {Department} from "../../classes/department";
import {DepartmentProviderService} from "../../services/department-provider.service";

@Component({
  selector: 'app-department-controls',
  templateUrl: './department-controls.component.html',
  styleUrls: ['./department-controls.component.css']
})
export class DepartmentControlsComponent implements OnInit {
  @Input() department: Department = new Department();
  constructor(private departmentProvider: DepartmentProviderService) { }

  ngOnInit(): void {
  }

}
