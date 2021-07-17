import {NgModule} from "@angular/core";
import {HeaderComponent} from "./header.component";
import {MatToolbarModule} from "@angular/material/toolbar";
import {MatIconModule} from "@angular/material/icon";
import {MatButtonModule} from "@angular/material/button";
import {StyleHelperModule} from "../shared/style-helper/style-helper.module";
import {RouterModule} from "@angular/router";

@NgModule({
  declarations: [HeaderComponent],
  imports: [
    RouterModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    StyleHelperModule
  ],
  exports: [HeaderComponent]
})
export class HeaderModule {

}
