import { Observable } from 'rxjs';
import { AuthRequest } from './models/auth-request.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService as SocialAuthService, SocialUser } from 'angularx-social-login';
import { api, storage } from './constants/api.constants';
import { ActivatedRoute, Router } from '@angular/router';
import { IAuthResponse } from './models/auth-response.model';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private returnUrl: string;

  public constructor(
    private httpClient: HttpClient,
    private socialAuthService: SocialAuthService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {}

  public isAuthenticated(): boolean {
    const token: string = localStorage.length > 0 ? localStorage.getItem(storage.token) : null;

    return !!token;
  }

  public externalLogin(providerId: string): Promise<any> {
    this.returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/';

    return this.socialAuthService.signIn(providerId).then((socialUser: SocialUser) => {
      const requestUrl: string = `${api.authenticate}/${providerId.toLocaleLowerCase()}`;

      this.httpClient.post(requestUrl, { idToken: socialUser.idToken }).subscribe((authResponse: IAuthResponse) => {
        localStorage.setItem(storage.token, JSON.stringify(authResponse.token));
        localStorage.setItem(storage.currentUser, JSON.stringify(authResponse.user));

        this.router.navigate([this.returnUrl]);
      });
    });
  }

  public authenticate(authRequest: AuthRequest): Observable<any> {
    this.returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/';
    const requestUrl: string = `${api.authenticate}`;

    return this.httpClient.post(requestUrl, authRequest).pipe(
      tap((authResponse: IAuthResponse) =>{
        localStorage.setItem(storage.token, JSON.stringify(authResponse.token));
        localStorage.setItem(storage.currentUser, JSON.stringify(authResponse.user));

        this.router.navigate([this.returnUrl]);
      }),
    );
  }

  public logout(): void {
    localStorage.removeItem(storage.token);
    localStorage.removeItem(storage.currentUser);
  }
}
