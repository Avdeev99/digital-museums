import { Component, Input, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'app-field-error',
    templateUrl: './error.component.html',
    styleUrls: ['./error.component.scss'],
})
export class CustomErrorComponent implements OnInit {
    @Input() public control: FormControl;

    public errorMessage: string;

    public constructor(private translateService: TranslateService) { }

    public ngOnInit(): void { }

    public hasError(): boolean {
        const hasError: boolean = !!this.control?.errors;
        if (!hasError) {
            return false;
        }

        const errorKey: string = Object.keys(this.control.errors)[0];

        switch (errorKey) {
            case 'required': {
                this.errorMessage = this.translateService.instant('shared.validation-errors.required');
                break;
            }
            case 'passwordsNotMatched': {
                this.errorMessage = this.translateService.instant('shared.validation-errors.passwordsNotMatched');
                break;
            }
        }

        return true;
    }
}
