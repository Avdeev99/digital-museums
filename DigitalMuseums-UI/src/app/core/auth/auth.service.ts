import { from, Observable } from 'rxjs';
import { AuthRequest } from './models/auth-request.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService as SocialAuthService, SocialUser } from 'angularx-social-login';
import { api, storage } from './constants/api.constants';
import { ActivatedRoute, Router } from '@angular/router';
import { IAuthResponse } from './models/auth-response.model';
import { switchMap, tap } from 'rxjs/operators';
import { CurrentUserService } from '../shared/services/current-user.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private returnUrl: string;

  public constructor(
    private httpClient: HttpClient,
    private socialAuthService: SocialAuthService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private currentUserService: CurrentUserService,
  ) {}

  public isAuthenticated(): boolean {
    const token: string = localStorage.length > 0 ? localStorage.getItem(storage.token) : null;

    return !!token;
  }

  public externalAuth(providerId: string): Observable<any> {
    this.returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/';

    return from(this.socialAuthService.signIn(providerId)).pipe(
      switchMap((socialUser: SocialUser) => {
        const requestUrl: string = `${api.authenticate}/${providerId.toLocaleLowerCase()}`;

        return this.httpClient.post(requestUrl, { idToken: socialUser.idToken });
      }),
      tap((authResponse: IAuthResponse) => {
        localStorage.setItem(storage.token, JSON.stringify(authResponse.token));
        this.currentUserService.setUser(authResponse.user);

        this.router.navigate([this.returnUrl]);
      })
    );
  }

  public authenticate(authRequest: AuthRequest): Observable<any> {
    this.returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/';
    const requestUrl: string = `${api.authenticate}`;

    return this.httpClient.post(requestUrl, authRequest).pipe(
      tap((authResponse: IAuthResponse) =>{
        localStorage.setItem(storage.token, JSON.stringify(authResponse.token));
        this.currentUserService.setUser(authResponse.user);

        this.router.navigate([this.returnUrl]);
      }),
    );
  }

  public logout(): void {
    this.currentUserService.logoutUser();
  }
}
