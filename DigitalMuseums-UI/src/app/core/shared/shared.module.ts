import { LocationService } from './services/location.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';

import { CustomButtonComponent } from './components/button/button.component';
import { TranslateModule } from '@ngx-translate/core';
import { MatSelectModule } from '@angular/material/select';
import { SubmenuComponent } from './components/submenu/submenu.component';
import { RouterModule } from '@angular/router';
import { SelectLanguageComponent } from './components/select-language/select-language.component';
import { CustomFormModule } from '../form/custom-form.module';

const services: Array<any> = [
  LocationService,
]

@NgModule({
  declarations: [
    CustomButtonComponent,
    SubmenuComponent,
    SelectLanguageComponent
  ],
  imports: [
    RouterModule,
    CommonModule,
    MatButtonModule,
    MatSelectModule, 
    TranslateModule,
    MatMenuModule,
    CustomFormModule,
  ],
  exports: [
    CustomButtonComponent,
    TranslateModule,
    MatSelectModule,
    SubmenuComponent,
    SelectLanguageComponent,
  ],
  providers: [
    ...services,
  ]
})
export class SharedModule {}
