import { Component, Injector, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem } from 'src/app/core/shared/models/menu-item.model';
import { ExhibitionInstructionalModel, InfoScreenType } from '../../models/exhibition.model';
import { ExhibitionStepBase } from '../exhibition-step-base/exhibition-step-base';

declare var carousel: any;

@Component({
    selector: 'app-exhibition-instructional-screen',
    templateUrl: './exhibition-instructional-screen.component.html',
    styleUrls: ['./exhibition-instructional-screen.component.scss']
})
export class ExhibitionInstructionalScreenComponent extends ExhibitionStepBase {

    @Input()
    data: ExhibitionInstructionalModel;

    public menuList: Array<MenuItem>;

    private backUrl: string;
    private museumId: number;

    constructor(
        injector: Injector,
        private router: Router,
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.museumId = this.data.exhibition.museumId;

        this.checkNavigationState();
        carousel();
    }

    private checkNavigationState(): void {
        const currentNavigationState: any = this.router.getCurrentNavigation();

        if (currentNavigationState && currentNavigationState.extras && currentNavigationState.extras.state) {
            this.backUrl = currentNavigationState.extras.state.backUrl;
        }

        this.initMenuList();
    }

    public get exhibitionImages(): string[] {
        return this.data.exhibition && this.data.exhibition.imagePaths.length ? this.data.exhibition.imagePaths: null;
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
