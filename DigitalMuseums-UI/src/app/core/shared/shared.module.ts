import { LocationService } from './services/location.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';

import { CustomButtonComponent } from './components/button/button.component';
import { TranslateModule } from '@ngx-translate/core';
import { MatSelectModule } from '@angular/material/select';

const services: Array<any> = [
  LocationService,
]

@NgModule({
  declarations: [CustomButtonComponent],
  imports: [
    CommonModule,
    MatButtonModule,
    MatSelectModule, 
    TranslateModule,
  ],
  exports: [
    CustomButtonComponent,
    TranslateModule,
    MatSelectModule,
  ],
  providers: [
    ...services,
  ]
})
export class SharedModule {}
