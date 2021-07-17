import { NgModule } from '@angular/core';
import {LoadingSpinnerComponent} from "./loading-spinner/loading-spinner.component";
import {FullScreenLoadingSpinnerComponent} from "./loading-spinner/full-screen-loading-spinner/full-screen-loading-spinner.component";
import { FullContainerLoadingSpinnerComponent } from './loading-spinner/full-container-loading-spinner/full-container-loading-spinner.component';



@NgModule({
  declarations: [
    LoadingSpinnerComponent,
    FullScreenLoadingSpinnerComponent,
    FullContainerLoadingSpinnerComponent],
  imports: [],
  exports: [
    FullScreenLoadingSpinnerComponent
  ]
})
export class LoadingInformationModule { }
