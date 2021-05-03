import { Component, ComponentFactoryResolver, OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute, NavigationEnd } from '@angular/router';
import { Subscription } from 'rxjs';
import { filter } from 'rxjs/operators';
import { NavigationService } from 'src/app/core/shared/services/navigation.service';
import { ExhibitionStepContainerDirective } from '../../directives/exhibition-step-container.directive';
import { Exhibition, ExhibitionStepComponentsType, ExhibitionStepUrlParamsModel, StepsComponentModel } from '../../models/exhibition.model';
import { ExhibitionService } from '../../services/exhibition.service';
import { PrepareExhibitionService } from '../../services/prepare-exhibition.service';

@Component({
    selector: 'app-exhibition',
    templateUrl: './exhibition.component.html',
    styleUrls: ['./exhibition.component.scss']
})
export class ExhibitionComponent implements OnInit {
    exhibition: Exhibition;
    exhibitionId: number;

    @ViewChild(ExhibitionStepContainerDirective, { static: true }) content: ExhibitionStepContainerDirective;

    steps: StepsComponentModel<ExhibitionStepComponentsType>[];
    stepsSubscription: Subscription;
    activeStepIndex: number;
    activeStepSubscription: Subscription;
    routesSubscription: Subscription;
    isWorking: boolean;

    constructor(
        private router: Router,
        private activeRoute: ActivatedRoute,
        private resolver: ComponentFactoryResolver,
        private exhibitionService: ExhibitionService,
        private prepareExhibitionService: PrepareExhibitionService,
        private navigationService: NavigationService,
    ) { 
        this.setExhibitionId();
    }

    ngOnInit(): void {

        this.prepareExhibitionService.getExhibition(this.exhibitionId).subscribe(exhibition => {
            this.exhibition = exhibition;

            this.prepareExhibitionStepsData();
            this.getSteps();
            this.getActiveStep();
            this.startWatchingRouteChanges();
        });
    }

    prepareExhibitionStepsData(): void {
        this.exhibitionService.setExhibitionData(this.exhibition);
        this.exhibitionService.setExhibitionSteps(
            this.prepareExhibitionService.prepareExhibitionStepsList(this.exhibition),
        );
        this.exhibitionService.setFirstStepToShow();
    }

    getSteps(): void {
        this.stepsSubscription = this.exhibitionService.getExhibitionSteps()
            .subscribe((steps: StepsComponentModel<ExhibitionStepComponentsType>[]) => {
                this.steps = steps;
            });
    }

    getActiveStep(): void {
        this.activeStepSubscription = this.exhibitionService.getCurrentStepIndex()
            .subscribe((index: number) => {
                this.activeStepIndex = index;
                this.processActiveStep(index);
            });
    }

    startWatchingRouteChanges(): void {
        this.routesSubscription = this.router.events
            .pipe(filter((event: any) => event instanceof NavigationEnd))
            .subscribe(() => {
                const activeStep = this.steps[this.activeStepIndex];
                const { step } = this.activeRoute.snapshot.queryParams;

                if (!!step && activeStep.id !== step) {
                    this.goToStep(Number(step));
                }
            });
    }

    private processActiveStep(activeStep: number): void {
        debugger;
        const { component, id, data } = this.steps[activeStep];
        let componentRef;

        if (!!component) {
            const componentFactory = this.resolver.resolveComponentFactory<ExhibitionStepComponentsType>(component);

            // createComponent method is async and blocks JS stream. We should wait while current component
            // will be built to start building a new one.
            if (this.isWorking) {
                setTimeout(() => {
                    this.processActiveStep(activeStep);
                }, 100);
                return;
            }

            this.isWorking = true;
            this.content.containerRef.clear();
            componentRef = this.content.containerRef.createComponent(componentFactory);
            componentRef.instance.data = data;
            this.updateStepUrlTitle(id.toString());
            this.isWorking = false;
        }
    }


    private updateStepUrlTitle(title: string): void {
        const prepareParams: ExhibitionStepUrlParamsModel = {
            step: title,
        };

        this.navigationService.addQueryParameters(prepareParams);
    }

    private goToStep(stepId: number): void {
        const stepIndex = this.steps
            .findIndex((s: StepsComponentModel<ExhibitionStepComponentsType>) => s.id === stepId);
        const questionIndex = this.exhibitionService.getCurrentStepsTrackerNumberByExhibitId(stepId);

        this.exhibitionService.setCurrentStepIndex(stepIndex);
        this.exhibitionService.setCurrentStepsTrackerNumber(questionIndex);
        this.processActiveStep(stepIndex);
    }

    private setExhibitionId(): void {
        this.exhibitionId = this.activeRoute.snapshot.params.id;
    }
}
