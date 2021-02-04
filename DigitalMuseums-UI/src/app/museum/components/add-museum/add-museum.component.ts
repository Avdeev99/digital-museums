import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Observable, Subject } from 'rxjs';
import { LocationService } from 'src/app/core/shared/services/location.service';
import { IOption } from 'src/app/core/form/form.interface';
import { debounceTime, takeUntil } from 'rxjs/operators';
import { GenreService } from 'src/app/core/shared/services/genre.service';
import { Museum } from '../../models/museum.model';
import { MuseumService } from '../../services/museum.service';

@Component({
  selector: 'app-add-museum',
  templateUrl: './add-museum.component.html',
  styleUrls: ['./add-museum.component.scss']
})
export class AddMuseumComponent implements OnInit {
  public formGroup: FormGroup;

  public countries$: Observable<Array<IOption>>;
  public regions$: Observable<Array<IOption>>;
  public cities$: Observable<Array<IOption>>;
  public genres$: Observable<Array<IOption>>;

  private selectedImages: FileList;

  public constructor(
    private fb: FormBuilder,
    private locationService: LocationService,
    private genreService: GenreService,
    private museumService: MuseumService,
    ) {}

  private unsubscribe$: Subject<void> = new Subject();

  public ngOnInit(): void {
    this.initForm();
    this.initDropdowns();
    this.initSubscriptions();
  }

  public ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  public onSubmit(): void {
    if (this.formGroup.invalid) {
    }

    const museum: Museum = {
      ...this.formGroup.getRawValue(),
      images: this.selectedImages,
    };

    this.museumService.create(museum).subscribe();
  }

  onSelectFile(files: FileList) {
    if (files.length === 0)
        return;

    this.selectedImages = files;
}

  private initForm(): void {
    this.formGroup = this.fb.group({
      name: new FormControl(null, [Validators.required]),
      description: new FormControl(null, [Validators.required]),
      countryId: new FormControl(null, [Validators.required]),
      regionId: new FormControl(null, [Validators.required]),
      cityId: new FormControl(null, [Validators.required]),
      address: new FormControl(null, [Validators.required]),
      genreId: new FormControl(null).setValidators([Validators.required]),
      images: new FormControl(null).setValidators([Validators.required]),
    });
  }

  private initDropdowns(): void {
    this.countries$ = this.locationService.getCountries();
    this.genres$ = this.genreService.getAll();
  }

  private initSubscriptions(): void {
    this.formGroup.controls.countryId.valueChanges
      .pipe(takeUntil(this.unsubscribe$), debounceTime(500))
      .subscribe((countryId: number) => {
        this.regions$ = this.locationService.getRegionsByCountry(countryId);
      });

    this.formGroup.controls.regionId.valueChanges
      .pipe(takeUntil(this.unsubscribe$), debounceTime(500))
      .subscribe((regionId: number) => {
        this.cities$ = this.locationService.getCitiesByRegion(regionId);
      });
  }
}
