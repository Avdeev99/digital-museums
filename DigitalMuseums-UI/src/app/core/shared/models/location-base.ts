import { AbstractControl } from "@angular/forms";
import { Observable, Subject } from "rxjs";
import { takeUntil } from "rxjs/operators";
import { IOption } from "../../form/form.interface";
import { LocationService } from "../services/location.service";

export abstract class LocationBase {
    public countries$: Observable<Array<IOption>>;
    public regions$: Observable<Array<IOption>>;
    public cities$: Observable<Array<IOption>>;

    protected unsubscribe$: Subject<void> = new Subject();

    constructor(
        protected locationService: LocationService,
    ) {
        this.initLocationDropdowns();
    }

    protected initLocationDropdowns(): void {
        this.countries$ = this.locationService.getCountries();
    }

    protected countryValueChanges(countryId: AbstractControl): void {
        countryId.valueChanges
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe((countryId: number) => {
                this.regions$ = this.locationService.getRegionsByCountry(countryId);
            });
    }

    protected regionValueChanges(regionId: AbstractControl): void {
        regionId.valueChanges
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe((regionId: number) => {
                this.cities$ = this.locationService.getCitiesByRegion(regionId);
            });
    }
}