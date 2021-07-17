import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AreYouSureDialogComponent } from './are-you-sure-dialog/are-you-sure-dialog.component';
import {MatDialogModule} from "@angular/material/dialog";
import {InformationDialogComponent} from "./information-dialog/information-dialog.component";
import {SingleStringDialogComponent} from "./single-string-dialog/single-string-dialog.component";
import {MatFormFieldModule} from "@angular/material/form-field";
import {FormsModule} from "@angular/forms";

@NgModule({
  declarations: [
    AreYouSureDialogComponent,
    InformationDialogComponent,
    SingleStringDialogComponent
  ],
  imports: [
    CommonModule,
    MatDialogModule,
    MatFormFieldModule,
    FormsModule
  ],
  exports: [
    AreYouSureDialogComponent,
    InformationDialogComponent,
    SingleStringDialogComponent
  ],
})
export class CommonDialogsModule { }
