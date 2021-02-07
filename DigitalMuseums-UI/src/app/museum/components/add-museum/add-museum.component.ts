import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Observable, Subject, throwError } from 'rxjs';
import { LocationService } from 'src/app/core/shared/services/location.service';
import { IOption } from 'src/app/core/form/form.interface';
import { catchError, debounceTime, takeUntil } from 'rxjs/operators';
import { GenreService } from 'src/app/core/shared/services/genre.service';
import { Museum } from '../../models/museum.model';
import { MuseumService } from '../../services/museum.service';
import { MuseumBase } from '../../models/museum-base';
import { ActivatedRoute, Router } from '@angular/router';
import { MuseumDetails } from '../../models/museum-details.model';

@Component({
  selector: 'app-add-museum',
  templateUrl: './add-museum.component.html',
  styleUrls: ['./add-museum.component.scss']
})
export class AddMuseumComponent extends MuseumBase implements OnInit, OnDestroy {
  public formGroup: FormGroup;
  public museum: MuseumDetails;

  private museumId: number;
  private selectedImages: FileList;

  public constructor(
    private fb: FormBuilder,
    protected locationService: LocationService,
    protected genreService: GenreService,
    private museumService: MuseumService,
    private route: ActivatedRoute,
    private router: Router,
    ) {
    super(locationService, genreService);
    this.setMuseumId();
  }

  public ngOnInit(): void {
    this.initForm();
    this.initDropdowns();
    this.initSubscriptions();
    this.fetchMuseum();
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
      id: this.museumId,
    };

    const museumRequest: Observable<any> = this.museum
      ? this.museumService.update(museum)
      : this.museumService.create(museum);

      museumRequest.subscribe();
  }

  public onSelectFile(files: FileList) {
    if (files.length === 0)
        return;

    this.selectedImages = files;
  }

  private setMuseumId(): void {
    this.museumId = this.route.snapshot.params.id;
  }

  private fetchMuseum(): void {
    if (this.museumId) {
      this.museumService.get(this.museumId)
        .pipe(
          catchError(err => {
            this.router.navigate(['museum']);
            return throwError(err);
          }),
        )
        .subscribe(data => {
          this.museum = data;
          this.formGroup.patchValue(data);
        });
    }
  }

  private initForm(): void {
    this.formGroup = this.fb.group({
      name: new FormControl(null, [Validators.required]),
      description: new FormControl(null, [Validators.required]),
      countryId: new FormControl(null, [Validators.required]),
      regionId: new FormControl(null, [Validators.required]),
      cityId: new FormControl(null, [Validators.required]),
      address: new FormControl(null, [Validators.required]),
      genreId: new FormControl(null, [Validators.required]),
      images: new FormControl(null, [Validators.required]),
    });
  }

  private initSubscriptions(): void {
    this.countryValueChanges(this.formGroup.controls.countryId);
    this.regionValueChanges(this.formGroup.controls.regionId);
  }
}
