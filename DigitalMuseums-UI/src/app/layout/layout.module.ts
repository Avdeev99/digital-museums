import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { FooterComponent } from './footer/footer.component';
import { HeaderComponent } from './header/header.component';
import { SharedModule } from '../core/shared/shared.module';
import { TranslateModule } from '@ngx-translate/core';
import { HomeComponent } from './home/home.component';
import { CustomFormModule } from '../core/form/custom-form.module';

@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent, 
    HomeComponent
  ],
  imports: [
    CustomFormModule,
    CommonModule,
    RouterModule,
    TranslateModule,
    SharedModule,
  ],
  exports: [
    HeaderComponent,
    FooterComponent
  ],
})
export class LayoutModule {}
