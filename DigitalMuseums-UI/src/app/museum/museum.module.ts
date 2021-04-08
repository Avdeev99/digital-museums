import { AddMuseumComponent } from './components/add-museum/add-museum.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../core/shared/shared.module';
import { CustomFormModule } from '../core/form/custom-form.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatTabsModule } from '@angular/material/tabs'
import { MatDialogModule } from '@angular/material/dialog'
import { MuseumRoutingModule } from './museum-routing.module';
import { MuseumDetailsComponent } from './components/museum-details/museum-details.component';
import { MuseumsComponent } from './components/museums/museums.component';
import { LinkingMuseumToUserComponent } from './components/linking-museum-to-user/linking-museum-to-user.component';

@NgModule({
  declarations: [AddMuseumComponent, MuseumDetailsComponent, MuseumsComponent, LinkingMuseumToUserComponent],
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
