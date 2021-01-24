import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';

import { CustomButtonComponent } from './components/button/button.component';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
  declarations: [CustomButtonComponent],
  imports: [
    CommonModule,
    MatButtonModule,
    TranslateModule,
  ],
  exports: [
    CustomButtonComponent,
    TranslateModule,
  ],
})
export class SharedModule {}
