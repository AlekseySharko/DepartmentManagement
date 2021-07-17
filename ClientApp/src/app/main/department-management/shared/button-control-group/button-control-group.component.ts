import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-button-control-group',
  templateUrl: './button-control-group.component.html',
  styleUrls: ['./button-control-group.component.css']
})
export class ButtonControlGroupComponent implements OnInit {
  @Output() onEditEvent: EventEmitter<any> = new EventEmitter<any>();
  @Output() onDeleteEvent: EventEmitter<any> = new EventEmitter<any>();

  ngOnInit(): void {
  }

  onEdit() {
    this.onEditEvent.emit();
  }

  onDelete() {
    this.onDeleteEvent.emit();
  }
}
