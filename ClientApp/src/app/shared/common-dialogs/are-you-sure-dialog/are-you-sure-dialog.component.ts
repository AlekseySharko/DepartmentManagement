import {Component, Inject } from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";

export interface AreYouSureDialogData {
  question: string,
  bold: string,
  boldNextLine: boolean,
  okButton: string,
  cancelButton: string;
}

@Component({
  templateUrl: './are-you-sure-dialog.component.html',
  styleUrls: ['./are-you-sure-dialog.component.css']
})
export class AreYouSureDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<AreYouSureDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: AreYouSureDialogData) {}

  onNoClick(): void {
    this.dialogRef.close();
  }
}
