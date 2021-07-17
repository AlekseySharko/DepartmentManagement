import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";

export interface SingleStringDialogData {
  question: string;
  label: string;
  information: string;
}
@Component({
  templateUrl: './single-string-dialog.component.html',
  styleUrls: ['./single-string-dialog.component.css']
})
export class SingleStringDialogComponent {

  constructor(
    public dialogRef: MatDialogRef<SingleStringDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: SingleStringDialogData) {}

  onNoClick(): void {
    this.dialogRef.close();
  }
}
