import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";

export interface InformationDialogData {
  bold: string,
  regular: string,
  textNotCenter: boolean
}
@Component({
  templateUrl: './information-dialog.component.html',
  styleUrls: ['./information-dialog.component.css']
})
export class InformationDialogComponent {

  constructor(
    public dialogRef: MatDialogRef<InformationDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: InformationDialogData) {}

  onNoClick(): void {
    this.dialogRef.close();
  }
}
