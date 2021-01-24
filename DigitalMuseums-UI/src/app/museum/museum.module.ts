import { AddMuseumComponent } from './components/add-museum/add-museum.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../core/shared/shared.module';
import { CustomFormModule } from '../core/form/custom-form.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatTabsModule } from '@angular/material/tabs'
import { MatDialogModule } from '@angular/material/dialog'
import { MuseumRoutingModule } from './museum-routing.module';

@NgModule({
  declarations: [AddMuseumComponent],
  imports: [
      CommonModule,
      FormsModule,
      ReactiveFormsModule,
      MatTabsModule,
      CustomFormModule,
      MatDialogModule,
      SharedModule,
      MuseumRoutingModule
    ],
})
export class MuseumModule {}
