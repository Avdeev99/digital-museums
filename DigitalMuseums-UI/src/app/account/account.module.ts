import { NgModule, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './components/login/login.component';
import { TestComponent } from './components/test/test.component';
import { AccountRoutingModule } from './account-routing.module';

@NgModule({
  declarations: [LoginComponent, TestComponent],
  imports: [CommonModule, AccountRoutingModule],
})
export class AccountModule {}
