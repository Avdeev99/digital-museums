import { Type } from '@angular/core';
import { ExhibitionFormComponent } from '../components/exbition-form/exhibition-form.component';
import { ExhibitionInstructionalScreenComponent } from '../components/exhibition-instructional-screen/exhibition-instructional-screen.component';
import { ExhibitionStepBase } from '../components/exhibition-step-base/exhibition-step-base';
import { ExhibitDetails } from './../../exhibit/models/exhibit-details.model';

export enum StepType {
    Info = 0,
    Exhibit = 1,
}

export enum InfoScreenType {
    Greeting = 'greeting',
    Finish = 'finish',
}

export enum InfoScreenStepNames {
    Welcome = 'Welcome',
    Complete = 'Finish',
}

export interface ExhibitionInstructionalModel {
    title: string;
    description: string;
    primaryButtonText: string;
    type: string;
}

export interface Exhibition {
    id: number;
    name: string;
    description: string;
    exhibits: ExhibitDetails[];
    museumId: number;
    tags: string[];
    imagePath: string;
}

export interface StepsComponentModel<T extends ExhibitionStepBase> {
    id: number;
    type: StepType;
    component: Type<T>;
    data: ExhibitionInstructionalModel | ExhibitDetails;
}

export type ExhibitionStepComponentsType = (
    ExhibitionFormComponent |
    ExhibitionInstructionalScreenComponent
);

export interface ExhibitionStepUrlParamsModel {
    step: string;
}

export interface ExhibitionFilter {
    name?: string;
    tags?: string[];
    museumId: number;
}

export interface ExhibitionEditing {
    id: number;
    name: string;
    description: string;
    exhibitIds: number[];
    museumId: number;
    tags: string[];
    images: FileList;
}