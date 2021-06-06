import { HttpErrorResponse } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { AuthRequest } from '../../../core/auth/models/auth-request.model';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { GoogleLoginProvider } from 'angularx-social-login';
import { Subject, throwError } from 'rxjs';
import { AuthService } from '../../../core/auth/auth.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit, OnDestroy {
    public formGroup: FormGroup;

    public isFetching: boolean = false;
    public resultMessageKey: string = 'account.login.errors.invalid-credentials';
    public isLoginFailed: boolean = null;

    public constructor(
        private fb: FormBuilder,
        private authService: AuthService) { }

    private unsubscribe$: Subject<void> = new Subject();

    public get googleProviderId(): string {
        return GoogleLoginProvider.PROVIDER_ID;
    }

    public ngOnInit(): void {
        this.initForm();
    }

    public ngOnDestroy(): void {
        this.unsubscribe$.next();
        this.unsubscribe$.complete();
    }

    public externalAuth(providerId: string): void {
        this.isFetching = true;

        this.authService.externalAuth(providerId).pipe(
            catchError((error: HttpErrorResponse) => {
                this.isFetching = false;
                this.isLoginFailed = true;

                return throwError(error);
            }),
        ).subscribe(() => {
            this.isFetching = false;
            this.isLoginFailed = false;
        });
    }

    public authenticate(): void {
        if (this.formGroup.invalid) {
            return;
        }

        this.isFetching = true;

        const authRequest: AuthRequest = this.formGroup.getRawValue();

        this.authService.authenticate(authRequest)
            .pipe(
                catchError((error: HttpErrorResponse) => {
                    this.isFetching = false;
                    this.isLoginFailed = true;

                    return throwError(error);
                }),
            ).subscribe(() => {
                this.isFetching = false;
                this.isLoginFailed = false;
            });
    }

    private initForm(): void {
        this.formGroup = this.fb.group({
            email: [null, [Validators.required, Validators.email]],
            password: [null, [Validators.required]],
        });
    }
}
