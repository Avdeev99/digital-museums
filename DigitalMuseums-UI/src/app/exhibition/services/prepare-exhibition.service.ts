import { TranslateService } from '@ngx-translate/core';
import { Observable, of } from 'rxjs';
import { ExhibitDetails } from './../../exhibit/models/exhibit-details.model';
import { Injectable } from "@angular/core";
import { ExhibitionFormComponent } from "../components/exbition-form/exhibition-form.component";
import { ExhibitionInstructionalScreenComponent } from "../components/exhibition-instructional-screen/exhibition-instructional-screen.component";
import { Exhibition, StepsComponentModel, ExhibitionStepComponentsType, StepType, InfoScreenStepNames, InfoScreenType, ExhibitionInstructionalModel } from "../models/exhibition.model";

@Injectable({
    providedIn: 'root',
})
export class PrepareExhibitionService {
    constructor() {}

    getExhibition(id: number): Observable<Exhibition> {
        return of({
            id: 1,
            name: 'Test Exhibition',
            description: 'Test Exhibition Description',
            museumId: 1,
            exhibits: [
                {
                    id: 1,
                    name: 'Exhibit 1',
                    description: 'Exhibit 1 Description',
                    author: 'Author 1',
                    museumId: 1,
                    tags: [],
                    imagePaths: [],
                    date: null,
                },
                {
                    id: 2,
                    name: 'Exhibit 2',
                    description: 'Exhibit 2 Description',
                    author: 'Author 2',
                    museumId: 1,
                    tags: [],
                    imagePaths: [],
                    date: null,
                }
            ]
        });
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
            title: exhibition.name,
            description: exhibition.description,
            primaryButtonText: screenTypePrefix === InfoScreenType.Greeting
                ? 'shared.buttons.start'
                : 'shared.buttons.goToMuseum',
            type: screenTypePrefix,
        };
    }
}