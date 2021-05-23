import { Component, ComponentFactoryResolver, OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute, NavigationEnd } from '@angular/router';
import { Subscription } from 'rxjs';
import { filter } from 'rxjs/operators';
import { MenuItem } from 'src/app/core/shared/models/menu-item.model';
import { NavigationService } from 'src/app/core/shared/services/navigation.service';
import { ExhibitionStepContainerDirective } from '../../directives/exhibition-step-container.directive';
import { Exhibition, ExhibitionStepComponentsType, ExhibitionStepUrlParamsModel, StepsComponentModel } from '../../models/exhibition.model';
import { ExhibitionService } from '../../services/exhibition.service';
import { PrepareExhibitionService } from '../../services/prepare-exhibition.service';

declare var carousel: any;

@Component({
    selector: 'app-exhibition',
    templateUrl: './exhibition.component.html',
    styleUrls: ['./exhibition.component.scss']
})
export class ExhibitionComponent implements OnInit {
    exhibition: Exhibition;
    exhibitionId: number;

    private backUrl: string;
    private museumId: number;

    @ViewChild(ExhibitionStepContainerDirective, { static: true }) content: ExhibitionStepContainerDirective;

    public menuList: Array<MenuItem>;

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
            this.museumId = exhibition.museumId;

            this.prepareExhibitionStepsData();
            this.getSteps();
            this.getActiveStep();
            this.startWatchingRouteChanges();
            this.checkNavigationState();
            carousel();
        });
    }

    public get exhibitionImages(): string[] {
        return this.exhibition && this.exhibition.imagePaths.length ? this.exhibition.imagePaths: null;
    }

    private checkNavigationState(): void {
        const currentNavigationState: any = this.router.getCurrentNavigation();
    
        if (currentNavigationState && currentNavigationState.extras && currentNavigationState.extras.state) {
          this.backUrl = currentNavigationState.extras.state.backUrl;
        }
    
        this.initMenuList();
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

    private initMenuList(): void {
        const state: { backUrl?: string } = this.backUrl ? { backUrl: this.backUrl } : {};
        this.menuList = [
          {
            name: 'menu.museum',
            href: `/museum/${this.museumId}`,
            state,
          },
          {
            name: 'menu.exhibits',
            href: `/exhibit/${this.museumId}/search`
          },
          {
            name: 'menu.souvenirs',
            href: `/souvenir/${this.museumId}/search`,
            state,
          },
        ];
      }
}
