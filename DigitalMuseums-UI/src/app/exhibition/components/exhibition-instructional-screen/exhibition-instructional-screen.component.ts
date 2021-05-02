import { Component, Injector, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { finalize } from 'rxjs/operators';
import { ExhibitionInstructionalModel, InfoScreenType } from '../../models/exhibition.model';
import { ExhibitionStepBase } from '../exhibition-step-base/exhibition-step-base';

@Component({
    selector: 'app-exhibition-instructional-screen',
    templateUrl: './exhibition-instructional-screen.component.html',
    styleUrls: ['./exhibition-instructional-screen.component.scss']
})
export class ExhibitionInstructionalScreenComponent extends ExhibitionStepBase {

    @Input()
    data: ExhibitionInstructionalModel;

    constructor(
        injector: Injector,
        private router: Router,
    ) {
        super(injector);
    }

    ngOnInit(): void {
    }

    get isFinalSceen(): boolean {
        return this.data.type === InfoScreenType.Finish;
    }

    handlePrimaryButtonClick(): void {
        if (this.data.type === InfoScreenType.Finish) {
            this.navigateToMuseum();

            return;
        }

        this.nextStep();
    }

    handleBack(): void {
        this.prevStep();
    }

    private navigateToMuseum(): void {
        const museumId: number = this.exhibitionService.getExhibitionMuseumId();
        this.router.navigate([`museum/${museumId}`]);
    }
}
