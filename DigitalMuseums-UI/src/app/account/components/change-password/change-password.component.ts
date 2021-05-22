import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { catchError } from 'rxjs/operators';
import { AccountService } from '../../services/account.service';

@Component({
    selector: 'app-change-password',
    templateUrl: './change-password.component.html',
    styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit {
    public formGroup: FormGroup;
    public resultMessageKey: string;
    public isPasswordChanged: boolean = null;

    constructor(
        private fb: FormBuilder,
        private accountService: AccountService,
    ) { }

    ngOnInit(): void {
        this.initForm();
    }

    public onSubmit(): void {
        if (this.formGroup.invalid) {
            return;
        }

        const changePasswordRequest = this.formGroup.getRawValue();

        this.accountService.changePassword(changePasswordRequest)
            .pipe(
                catchError(result => {
                    this.resultMessageKey = result.error.message;
                    this.formGroup.reset();
                    this.isPasswordChanged = false;
                    throw(result);
                })
            ).subscribe(() => {
                this.resultMessageKey = 'account.change-password.changed-successful';
                this.formGroup.reset();
                this.isPasswordChanged = true;
            });
    }

    public get isFormInvalid(): boolean {
        return this.formGroup.invalid;
    }

    public onPasswordChange(): void {
        const password = this.formGroup?.controls?.newPassword?.value;
        const confirmPasswordControl = this.formGroup?.controls?.newPasswordConfirmation;
        const confirmPassword = confirmPasswordControl?.value;

        if (password === confirmPassword) {
            confirmPasswordControl.setErrors(null);
        } else {
            confirmPasswordControl.setErrors({ 'passwordsNotMatched': true });
        }
    }

    private initForm(): void {
        this.formGroup = this.fb.group({
            oldPassword: new FormControl(null, [Validators.required, Validators.minLength(6)]),
            newPassword: new FormControl(null, [Validators.required, Validators.minLength(6)]),
            newPasswordConfirmation: new FormControl(null, [Validators.required, Validators.minLength(6)]),
        });
    }
}
