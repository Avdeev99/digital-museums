import { NgModule, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './components/login/login.component';
import { AccountRoutingModule } from './account-routing.module';
import { SharedModule } from '../core/shared/shared.module';
import { CustomFormModule } from '../core/form/custom-form.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatTabsModule } from '@angular/material/tabs'
import { MatDialogModule } from '@angular/material/dialog';
import { ChangePasswordComponent } from './components/change-password/change-password.component'

@NgModule({
  declarations: [
    LoginComponent,
    ChangePasswordComponent,
  ],
  imports: [
      CommonModule,
      FormsModule,
      ReactiveFormsModule,
      MatTabsModule,
      CustomFormModule,
      MatDialogModule,
      SharedModule,
      AccountRoutingModule
    ],
})
export class AccountModule {}
