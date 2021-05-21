import { LocationService } from './services/location.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { CustomButtonComponent } from './components/button/button.component';
import { TranslateModule } from '@ngx-translate/core';
import { MatSelectModule } from '@angular/material/select';
import { SubmenuComponent } from './components/submenu/submenu.component';
import { RouterModule } from '@angular/router';
import { SelectLanguageComponent } from './components/select-language/select-language.component';
import { CustomFormModule } from '../form/custom-form.module';
import { SpinnerComponent } from './components/spinner/spinner.component';

const services: Array<any> = [
  LocationService,
]

@NgModule({
  declarations: [
    CustomButtonComponent,
    SubmenuComponent,
    SelectLanguageComponent,
    SpinnerComponent,
  ],
  imports: [
    RouterModule,
    CommonModule,
    MatButtonModule,
    MatSelectModule, 
    TranslateModule,
    MatMenuModule,
    CustomFormModule,
    MatProgressSpinnerModule 
  ],
  exports: [
    RouterModule,
    CustomButtonComponent,
    TranslateModule,
    MatSelectModule,
    SubmenuComponent,
    SelectLanguageComponent,
    SpinnerComponent,
  ],
  providers: [
    ...services,
  ]
})
export class SharedModule {}
