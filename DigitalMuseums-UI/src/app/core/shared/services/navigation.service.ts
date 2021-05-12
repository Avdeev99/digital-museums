import { Injectable } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { ExhibitionStepUrlParamsModel } from "src/app/exhibition/models/exhibition.model";

@Injectable({
    providedIn: 'root',
})
export class NavigationService {
    constructor(
        private activeRoute: ActivatedRoute,
        private router: Router,
    ) {}

    addQueryParameters(queryParams: any, skipUrl?: boolean): void {
        const prepareParams = {
            ...this.activeRoute.snapshot.queryParams,
            ...queryParams,
        };

        this.navigateWithQuery(prepareParams, skipUrl);
    }

    private navigateWithQuery(queryParams: any, skipUrl?: boolean): void {
        const prepareNavigationParams = {
            queryParams,
            replaceUrl: skipUrl || false,
        };

        this.router.navigate([], prepareNavigationParams);
    }
}