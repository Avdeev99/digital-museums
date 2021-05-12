import { Injector } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ExhibitionService } from '../../services/exhibition.service';


export abstract class ExhibitionStepBase {

    protected exhibitionService: ExhibitionService;
    protected activatedRoute: ActivatedRoute;
    protected dialog: MatDialog;

    showLoading: boolean;

    constructor(injector: Injector) {
        this.exhibitionService = injector.get(ExhibitionService);
        this.activatedRoute = injector.get(ActivatedRoute);
        this.dialog = injector.get(MatDialog);
    }

    hasPrevStep(): boolean {
        return this.exhibitionService.hasPrevButton();
    }

    hasNextStep(): boolean {
        return this.exhibitionService.hasNextButton();
    }

    nextStep(): void {
        this.exhibitionService.moveToNext();
    }

    prevStep(): void {
        this.exhibitionService.moveToPrevious();
    }
}
