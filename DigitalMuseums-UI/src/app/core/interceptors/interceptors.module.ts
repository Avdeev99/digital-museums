import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthModule } from '../auth/auth.module';
import { ApiInterceptor } from './api.interceptor';
import { ErrorInterceptor } from './error.interceptor';

@NgModule({
  declarations: [],
  imports: [AuthModule, RouterModule, CommonModule, HttpClientModule],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ApiInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
})
export class InterceptorsModule {}
