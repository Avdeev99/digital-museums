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

const services: Array<any> = [
  LocationService,
]

@NgModule({
  declarations: [
    CustomButtonComponent,
    SubmenuComponent
  ],
  imports: [
    RouterModule,
    CommonModule,
    MatButtonModule,
    MatSelectModule, 
    TranslateModule,
    MatMenuModule,
  ],
  exports: [
    CustomButtonComponent,
    TranslateModule,
    MatSelectModule,
    SubmenuComponent
  ],
  providers: [
    ...services,
  ]
})
export class SharedModule {}
