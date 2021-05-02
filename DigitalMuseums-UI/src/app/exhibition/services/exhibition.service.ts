import { ExhibitionStepComponentsType, StepType } from './../models/exhibition.model';
import { ExhibitDetails } from './../../exhibit/models/exhibit-details.model';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

import { Exhibition, StepsComponentModel } from '../models/exhibition.model';


@Injectable({
    providedIn: 'root',
})
export class ExhibitionService {

    // exhibition data
    exhibition$$: BehaviorSubject<Exhibition>;
    // index of current step (with info screens)
    currentStepIndex$$: BehaviorSubject<number>;
    // first question to show after info screen
    initialExhibitId: number;
    // if the exhibition has InProgress status need to show fist unanswered question
    needToShowInitialExhibit: boolean;
    exhibitsList: ExhibitDetails[] = [];
    steps$$: BehaviorSubject<StepsComponentModel<ExhibitionStepComponentsType>[]>;

    // progress tracker data current_question/total
    // not including info screens
    currentExhibitNumber$$: BehaviorSubject<number>;

    constructor() {
        this.exhibition$$ = new BehaviorSubject(null);
        this.currentExhibitNumber$$ = new BehaviorSubject(0);
        this.currentStepIndex$$ = new BehaviorSubject(0);
        this.steps$$ = new BehaviorSubject([]);
    }

    // START EXHIBITION
    setExhibitionData(exhibition: Exhibition): void {
        this.exhibition$$.next(exhibition);
        this.exhibitsList = exhibition.exhibits;
    }

    getExhibitionId(): number {
        const exhibition = this.exhibition$$.getValue();

        return exhibition.id;
    }

    getExhibitionMuseumId(): number {
        const exhibition = this.exhibition$$.getValue();

        return exhibition.museumId;
    }
    // END EXHIBITION

    // START STEPS PROGRESS TRACKER
    setCurrentStepsTrackerNumber(stepNumber: number): void {
        this.currentExhibitNumber$$.next(stepNumber);
    }

    getCurrentStepsTrackerNumber(): Observable<number> {
        return this.currentExhibitNumber$$.asObservable();
    }

    getStepsTrackerCount(): number {
        return this.exhibitsList.length;
    }

    updateStepsTracker(value: number): void {
        this.setCurrentStepsTrackerNumber(value);
    }

    getCurrentStepsTrackerNumberByExhibitId(id: number): number {
        const exhibitIndex = this.exhibitsList.findIndex((q: ExhibitDetails) => q.id === id);

        return exhibitIndex + 1;
    }
    // END STEPS PROGRESS TRACKER

    // START STEPS
    setExhibitionSteps(steps: StepsComponentModel<ExhibitionStepComponentsType>[]): void {
        this.steps$$.next(steps);
    }

    getExhibitionSteps(): Observable<StepsComponentModel<ExhibitionStepComponentsType>[]> {
        return this.steps$$.asObservable();
    }

    getCurrentStepIndex(): Observable<number> {
        return this.currentStepIndex$$.asObservable();
    }

    setCurrentStepIndex(newId: number): void {
        this.currentStepIndex$$.next(newId);
    }

    hasPrevButton(): boolean {
        const currentIndex = this.currentStepIndex$$.getValue();

        return (
            this.getStepTypeByIndex(currentIndex) === StepType.Exhibit &&
            this.getStepTypeByIndex(currentIndex - 1) === StepType.Exhibit
        );
    }

    hasNextButton(): boolean {
        const currentIndex = this.currentStepIndex$$.getValue();

        return (
            this.getStepTypeByIndex(currentIndex) === StepType.Exhibit &&
            this.getStepTypeByIndex(currentIndex + 1) === StepType.Exhibit
        );
    }

    moveToNext(): void {
        const steps = this.steps$$.getValue();
        const currentStepIndex = this.currentStepIndex$$.getValue();
        const currentExhibitNumber = this.currentExhibitNumber$$.getValue();
        debugger;

        if (currentStepIndex + 1 < steps.length) {
            this.setCurrentStepIndex(currentStepIndex + 1);
            this.updateStepsTracker(currentExhibitNumber + 1);
        }
    }

    moveToPrevious(): void {
        if (this.needToShowInitialExhibit) {
            this.needToShowInitialExhibit = false;
            return;
        }

        const currentStepIndex = this.currentStepIndex$$.getValue();
        const currentExhibitNumber = this.currentExhibitNumber$$.getValue();

        if (currentStepIndex - 1 >= 0) {
            this.setCurrentStepIndex(currentStepIndex - 1);
            this.updateStepsTracker(currentExhibitNumber - 1);
        }
    }

    getCurrentStepProperty<T>(propertyName: string): T {
        const currentIndex = this.currentStepIndex$$.getValue();
        const steps = this.steps$$.getValue();

        return !!steps[currentIndex] ? steps[currentIndex][propertyName] : null;
    }

    setFirstStepToShow(): void {
        this.setCurrentStepIndex(0);
    }
    // END STEPS

    private getStepTypeByIndex(index: number): StepType {
        const steps = this.steps$$.getValue();

        return !!steps[index] ? steps[index].type : null;
    }
}
