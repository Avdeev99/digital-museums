import { Observable, Subject } from "rxjs";
import { IOption } from "src/app/core/form/form.interface";
import { LocationBase } from "src/app/core/shared/models/location-base";
import { GenreService } from "src/app/core/shared/services/genre.service";
import { LocationService } from "src/app/core/shared/services/location.service";

export abstract class MuseumBase extends LocationBase {
    public genres$: Observable<Array<IOption>>;

    protected unsubscribe$: Subject<void> = new Subject();

    constructor(
        protected locationService: LocationService,
        protected genreService: GenreService,
    ) {
        super(locationService);
        this.initDropdowns();
    }

    protected initDropdowns(): void {
        this.genres$ = this.genreService.getAll();
    }
}