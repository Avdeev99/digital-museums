import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field';
import { MatChipsModule } from '@angular/material/chips';
import { MatListModule } from '@angular/material/list';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatIconModule } from '@angular/material/icon';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSliderModule } from '@angular/material/slider';

import { CustomSelectComponent } from './controls/select/select.component';
import { CustomInputComponent } from './controls/input/input.component';
import { CustomTextareaComponent } from './controls/textarea/textarea.component';
import { CustomChipsComponent } from './controls/chips/chips.component';
import { CustomCheckboxComponent } from './controls/checkbox/checkbox.component';
import { CustomErrorComponent } from './controls/error/error.component';
import { SliderComponent } from './controls/slider/slider.component';
import { CheckboxGroupComponent } from './controls/checkbox-group/checkbox-group.component';

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
  ],
  imports: [
    CommonModule,
    MatSelectModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatAutocompleteModule,
    MatChipsModule,
    MatIconModule,
    MatCheckboxModule,
    MatSliderModule,
    MatListModule,
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
  ],
  providers: [{ provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { floatLabel: 'never' } }],
})
export class CustomFormModule {}
