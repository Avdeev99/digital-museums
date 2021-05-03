import { ExhibitDetails } from './../../../exhibit/models/exhibit-details.model';
import { Component, Injector, Input } from '@angular/core';
import { ExhibitionStepBase } from '../exhibition-step-base/exhibition-step-base';

// for dynamically component loading
import { TranslatePipe } from '@ngx-translate/core';

@Component({
    selector: 'app-exbition-form',
    templateUrl: './exhibition-form.component.html',
    styleUrls: ['./exhibition-form.component.scss']
})
export class ExhibitionFormComponent extends ExhibitionStepBase {
    @Input()
    data: ExhibitDetails;

    constructor(injector: Injector) {
        super(injector);
    }

    ngOnInit(): void {
    }

    get showNextBtn(): boolean {
        return this.hasNextStep();
    }

    get showPrevBtn(): boolean {
        return this.hasPrevStep();
    }

    handleBack(): void {
        this.prevStep();
    }

    handleNext(): void {
        this.nextStep();
    }
}
