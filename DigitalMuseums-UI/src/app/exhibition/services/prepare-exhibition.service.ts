import { TranslateService } from '@ngx-translate/core';
import { Observable, of } from 'rxjs';
import { ExhibitDetails } from './../../exhibit/models/exhibit-details.model';
import { Injectable } from "@angular/core";
import { ExhibitionFormComponent } from "../components/exbition-form/exhibition-form.component";
import { ExhibitionInstructionalScreenComponent } from "../components/exhibition-instructional-screen/exhibition-instructional-screen.component";
import { Exhibition, StepsComponentModel, ExhibitionStepComponentsType, StepType, InfoScreenStepNames, InfoScreenType, ExhibitionInstructionalModel } from "../models/exhibition.model";
import { HttpClient } from '@angular/common/http';
import { api } from 'src/app/core/shared/constants/api.constants';

@Injectable({
    providedIn: 'root',
})
export class PrepareExhibitionService {
    constructor(
        private httpClient: HttpClient,
    ) {}

    getExhibition(id: number): Observable<Exhibition> {
        const requestUrl: string = `${api.exhibition}/${id}`

        return this.httpClient.get<Exhibition>(requestUrl);
    }

    prepareExhibitionStepsList(exhibition: Exhibition): StepsComponentModel<ExhibitionStepComponentsType>[] {
        const exhibitSteps = [];

        exhibition.exhibits.forEach((exhibit: ExhibitDetails) => {
            exhibitSteps.push({
                id: exhibit.id,
                type: StepType.Exhibit,
                component: ExhibitionFormComponent,
                data: exhibit,
            });
        });

        return [
            {
                id: InfoScreenStepNames.Welcome,
                type: StepType.Info,
                component: ExhibitionInstructionalScreenComponent,
                data: this.createInstructionalData(exhibition, InfoScreenType.Greeting),
            },
            ...exhibitSteps,
            {
                id: InfoScreenStepNames.Complete,
                type: StepType.Info,
                component: ExhibitionInstructionalScreenComponent,
                data: this.createInstructionalData(exhibition, InfoScreenType.Finish),
            },
        ];
    }

    private createInstructionalData(exhibition: Exhibition, screenTypePrefix: string): ExhibitionInstructionalModel {
        return {
            exhibition: exhibition, 
            primaryButtonText: screenTypePrefix === InfoScreenType.Greeting
                ? 'shared.buttons.start'
                : 'shared.buttons.goToMuseum',
            type: screenTypePrefix,
        };
    }
}