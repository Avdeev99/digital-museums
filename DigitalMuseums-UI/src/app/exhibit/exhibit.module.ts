import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../core/shared/shared.module';
import { CustomFormModule } from '../core/form/custom-form.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatTabsModule } from '@angular/material/tabs'
import { MatDialogModule } from '@angular/material/dialog'
import { ExhibitRoutingModule } from './exhibit-routing.module';
import { ExhibitEditingComponent } from './components/exhibit-editing/exhibit-editing.component';
import { ExhibitDetailsComponent } from './components/exhibit-details/exhibit-details.component';
import { ExhibitSearchComponent } from './components/exhibit-search/exhibit-search.component';

@NgModule({
  declarations: [ExhibitEditingComponent, ExhibitDetailsComponent, ExhibitSearchComponent],
  imports: [
      CommonModule,
      FormsModule,
      ReactiveFormsModule,
      MatTabsModule,
      CustomFormModule,
      MatDialogModule,
      SharedModule,
      ExhibitRoutingModule,
    ],
})
export class ExhibitModule {}
