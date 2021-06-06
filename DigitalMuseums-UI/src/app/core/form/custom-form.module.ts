import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule, MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field';
import { MatChipsModule } from '@angular/material/chips';
import { MatListModule } from '@angular/material/list';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatIconModule } from '@angular/material/icon';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSliderModule } from '@angular/material/slider';
import {MatCardModule} from '@angular/material/card';

import { CustomSelectComponent } from './controls/select/select.component';
import { CustomInputComponent } from './controls/input/input.component';
import { CustomTextareaComponent } from './controls/textarea/textarea.component';
import { CustomChipsComponent } from './controls/chips/chips.component';
import { CustomCheckboxComponent } from './controls/checkbox/checkbox.component';
import { CustomErrorComponent } from './controls/error/error.component';
import { SliderComponent } from './controls/slider/slider.component';
import { CheckboxGroupComponent } from './controls/checkbox-group/checkbox-group.component';
import { ListComponent } from './list/list.component';
import { MatButtonModule } from '@angular/material/button';
import { ConfirmDialogComponent } from './confirm-dialog/confirm-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '../shared/shared.module';
import { FileUploadComponent } from './controls/file-upload/file-upload.component';
import { MatFileUploadModule } from 'mat-file-upload';

@NgModule({
  declarations: [
    CustomSelectComponent,
    CustomInputComponent,
    CustomTextareaComponent,
    CustomChipsComponent,
    CustomCheckboxComponent,
    CustomErrorComponent,
    SliderComponent,
    CheckboxGroupComponent,
    ListComponent,
    ConfirmDialogComponent,
    FileUploadComponent,
  ],
  imports: [
    CommonModule,
    MatSelectModule,
    FormsModule,
    ReactiveFormsModule,
    MatAutocompleteModule,
    MatChipsModule,
    MatIconModule,
    MatCheckboxModule,
    MatSliderModule,
    MatListModule,
    MatCardModule,
    MatButtonModule,
    MatDialogModule,
    TranslateModule,
    MatFileUploadModule,
    MatFormFieldModule, 
    MatInputModule,
  ],
  exports: [
    CustomSelectComponent,
    CustomInputComponent,
    CustomTextareaComponent,
    CustomChipsComponent,
    CustomCheckboxComponent,
    CustomErrorComponent,
    SliderComponent,
    CheckboxGroupComponent,
    MatCardModule,
    ListComponent,
    MatIconModule,
    MatButtonModule,
    MatDialogModule,
    FileUploadComponent,
    MatFormFieldModule, 
    MatInputModule
  ],
  providers: [{ provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { floatLabel: 'never' } }],
})
export class CustomFormModule {}
