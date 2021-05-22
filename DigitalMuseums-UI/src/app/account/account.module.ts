import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './components/login/login.component';
import { AccountRoutingModule } from './account-routing.module';
import { SharedModule } from '../core/shared/shared.module';
import { CustomFormModule } from '../core/form/custom-form.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatTabsModule } from '@angular/material/tabs'
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatDialogModule } from '@angular/material/dialog';
import { ChangePasswordComponent } from './components/change-password/change-password.component';
import { UserInfoEditingComponent } from './components/user-info-editing/user-info-editing.component';
import { UserMuseumComponent } from './components/user-museum/user-museum.component';
import { AdminPortalComponent } from './components/admin-portal/admin-portal.component'

@NgModule({
  declarations: [
    LoginComponent,
    ChangePasswordComponent,
    UserInfoEditingComponent,
    UserMuseumComponent,
    AdminPortalComponent,
  ],
  imports: [
      CommonModule,
      FormsModule,
      ReactiveFormsModule,
      MatTabsModule,
      CustomFormModule,
      MatDialogModule,
      MatToolbarModule,
      MatTabsModule,
      SharedModule,
      AccountRoutingModule
    ],
})
export class AccountModule {}
