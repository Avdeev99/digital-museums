import { Component, OnInit } from '@angular/core';
import { GoogleLoginProvider } from 'angularx-social-login';
import { AuthService } from '../../../core/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  public constructor(private authService: AuthService) {}

  public get googleProviderId(): string {
    return GoogleLoginProvider.PROVIDER_ID;
  }

  public ngOnInit(): void {}

  public externalLogin(providerId: string): void {
    this.authService.externalLogin(providerId);
  }
}
