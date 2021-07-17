import {Component, EventEmitter, Input, OnDestroy, OnInit, Output} from '@angular/core';
import {Subject, Subscription} from "rxjs";
import {DepartmentProviderService} from "../../../services/department-provider.service";

@Component({
  selector: 'app-position-input',
  templateUrl: './position-input.component.html',
  styleUrls: ['./position-input.component.css']
})
export class PositionInputComponent implements OnInit, OnDestroy {
  @Input() position: string = '';
  @Output() positionChange: EventEmitter<string> = new EventEmitter<string>();
  @Input() departmentIdChangedEvent: Subject<number> = new Subject<number>();
  @Input() initialDepartmentId: number = 0;
  departmentSubscription = new Subscription();
  positionSubscription = new Subscription();
  existingPositions: string[] = [];
  filteredPositions: string[] = [];
  constructor(private departmentProvider: DepartmentProviderService) { }

  ngOnInit(): void {
    this.getPositions(this.initialDepartmentId);
    this.departmentSubscription = this.departmentIdChangedEvent.subscribe(data => {
      this.getPositions(data);
    })
  }
  ngOnDestroy(): void {
    this.departmentSubscription.unsubscribe();
    this.positionSubscription.unsubscribe();
  }
  inputChanged(value: string) {
    this.setUpAutocomplete(value);
    this.positionChange.emit(value);
  }
  private getPositions(departmentId: number) {
    this.positionSubscription = this.departmentProvider.getExistingPositions(departmentId).subscribe(data => {
      this.existingPositions = data;
      this.setUpAutocomplete("");
    })
  }
  private setUpAutocomplete(value: string) {
    this.filteredPositions = this.existingPositions?.filter(p => p.toLowerCase().includes(value.toLowerCase()));
  }
}
