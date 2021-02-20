import { AbstractControl } from "@angular/forms";
import { Observable, Subject } from "rxjs";
import { debounceTime, takeUntil } from "rxjs/operators";
import { IOption } from "src/app/core/form/form.interface";
import { GenreService } from "src/app/core/shared/services/genre.service";
import { LocationService } from "src/app/core/shared/services/location.service";

export abstract class MuseumBase {
    public countries$: Observable<Array<IOption>>;
    public regions$: Observable<Array<IOption>>;
    public cities$: Observable<Array<IOption>>;
    public genres$: Observable<Array<IOption>>;

    protected unsubscribe$: Subject<void> = new Subject();

    constructor(
        protected locationService: LocationService,
        protected genreService: GenreService,
    ) {
        this.initDropdowns();
    }

    protected initDropdowns(): void {
        this.countries$ = this.locationService.getCountries();
        this.genres$ = this.genreService.getAll();
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