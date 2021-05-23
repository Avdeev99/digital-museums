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
import { ExhibitListComponent } from './components/exhibit-list/exhibit-list.component';

@NgModule({
  declarations: [ExhibitEditingComponent, ExhibitDetailsComponent, ExhibitSearchComponent, ExhibitListComponent],
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
  exports: [
    ExhibitDetailsComponent
  ]
})
export class ExhibitModule {}
