import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FooterComponent } from './footer/footer.component';
import {HeaderModule} from "./header/header.module";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {MainModule} from "./main/main.module";
import {HttpClientModule} from "@angular/common/http";
import {MatDialog, MatDialogModule} from "@angular/material/dialog";
import {getRussianPaginatorIntl} from "./core/paginator-intl/russian-paginator-intl"
import {MatPaginatorIntl} from "@angular/material/paginator";

@NgModule({
  declarations: [
    AppComponent,
    FooterComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    HeaderModule,
    MainModule,
    MatDialogModule,
    AppRoutingModule
  ],
  providers: [
    {provide: MatPaginatorIntl, useValue: getRussianPaginatorIntl()}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
