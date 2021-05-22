import { ExhibitionEditing, ExhibitionFilter, ExhibitionStepComponentsType, StepType } from './../models/exhibition.model';
import { ExhibitDetails } from './../../exhibit/models/exhibit-details.model';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { serialize } from "object-to-formdata";
import { Exhibition, StepsComponentModel } from '../models/exhibition.model';
import { HttpClient, HttpParams } from '@angular/common/http';
import { api } from 'src/app/core/shared/constants/api.constants';


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

    constructor(
        private httpClient: HttpClient,
    ) {
        this.exhibition$$ = new BehaviorSubject(null);
        this.currentExhibitNumber$$ = new BehaviorSubject(0);
        this.currentStepIndex$$ = new BehaviorSubject(0);
        this.steps$$ = new BehaviorSubject([]);
    }

    public getFiltered(filter: ExhibitionFilter): Observable<Array<Exhibition>> {
        let httpParams = new HttpParams();
        Object.keys(filter).forEach((key) => {
            if (!!filter[key]) {
                httpParams = httpParams.append(key, filter[key]);
            }
        });

        return this.httpClient.get<Array<Exhibition>>(api.exhibition, { params: httpParams });
    }

    public get(id: number): Observable<Exhibition> {
        const requestUrl: string = `${api.exhibition}/${id}`

        return this.httpClient.get<Exhibition>(requestUrl);
    }

    public create(exhibition: ExhibitionEditing): Observable<any> {
        const formData: FormData = this.getFormData(exhibition);

        return this.httpClient.post(api.exhibition, formData);
    }

    public update(exhibition: ExhibitionEditing): Observable<any> {
        const formData: FormData = this.getFormData(exhibition);

        return this.httpClient.put(`${api.exhibition}/${exhibition.id}`, formData);
    }

    public delete(id: number): Observable<void> {
        const requestUrl: string = `${api.exhibition}/${id}`

        return this.httpClient.delete<void>(requestUrl);
    }

    private getFormData(exhibit: ExhibitionEditing): FormData {
        const formData: FormData = serialize(exhibit);
        Array.from(exhibit.images).forEach(image => {
            formData.append('images', image, image.name);
        });

        return formData;
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
