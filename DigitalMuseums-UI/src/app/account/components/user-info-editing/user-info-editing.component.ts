import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { catchError } from 'rxjs/operators';
import { storage } from 'src/app/core/auth/constants/api.constants';
import { AuthUser } from 'src/app/core/auth/models/auth-user.model';
import { CurrentUserService } from 'src/app/core/shared/services/current-user.service';
import { AccountService } from '../../services/account.service';

@Component({
    selector: 'app-user-info-editing',
    templateUrl: './user-info-editing.component.html',
    styleUrls: ['./user-info-editing.component.scss']
})
export class UserInfoEditingComponent implements OnInit {
    public formGroup: FormGroup;
    public resultMessageKey: string;
    public isInfoChanged: boolean = null;

    constructor(
        private fb: FormBuilder,
        private accountService: AccountService,
        private currentUserService: CurrentUserService,
    ) { }

    ngOnInit(): void {
        this.initForm();
    }

    public onSubmit(): void {
        if (this.formGroup.invalid) {
            return;
        }

        const userInfo = this.formGroup.getRawValue();

        this.accountService.editPersonalInfo(userInfo.name)
            .pipe(
                catchError(result => {
                    this.resultMessageKey = result.error.message;
                    this.isInfoChanged = false;
                    
                    throw (result);
                })
            ).subscribe(() => {
                this.resultMessageKey = 'account.user-info.changed-successful';
                this.isInfoChanged = true;
                this.fetchUserInfo();
            });
    }

    public get isFormInvalid(): boolean {
        return this.formGroup.invalid;
    }

    private initForm(): void {
        this.formGroup = this.fb.group({
            name: new FormControl(null, [Validators.required]),
            email: new FormControl({ value: '', disabled: true }, [Validators.required]),
        });

        this.fillForm();
    }

    private fetchUserInfo(): void {
        this.accountService.getCurrentUser().subscribe((user: AuthUser) => {
            localStorage.setItem(storage.currentUser, JSON.stringify(user));
            this.fillForm();
        });
    }

    private fillForm(): void {
        const user: AuthUser = this.currentUserService.getUser();

        this.formGroup.patchValue({
            name: user.userName,
            email: user.email,
        });
    }
}
