import { Component, Inject, OnDestroy, OnInit, Optional } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Observable, throwError } from 'rxjs';
import { LocationService } from 'src/app/core/shared/services/location.service';
import { catchError } from 'rxjs/operators';
import { GenreService } from 'src/app/core/shared/services/genre.service';
import { Museum } from '../../models/museum.model';
import { MuseumService } from '../../services/museum.service';
import { MuseumBase } from '../../models/museum-base';
import { ActivatedRoute, Router } from '@angular/router';
import { MuseumDetails } from '../../models/museum-details.model';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
    selector: 'app-add-museum',
    templateUrl: './add-museum.component.html',
    styleUrls: ['./add-museum.component.scss']
})
export class AddMuseumComponent extends MuseumBase implements OnInit, OnDestroy {
    public formGroup: FormGroup;
    public museum: MuseumDetails;
    public isFetching: boolean;

    public resultMessageKey: string;
    public isInfoChanged: boolean = null;

    public get isModal(): boolean {
        return !!this.dialogData && !!this.dialogData.museumId;
    }

    private museumId: number;
    private selectedImages: FileList;

    public constructor(
        private fb: FormBuilder,
        protected locationService: LocationService,
        protected genreService: GenreService,
        private museumService: MuseumService,
        private route: ActivatedRoute,
        private router: Router,
        @Optional() private dialogRef: MatDialogRef<AddMuseumComponent>,
        @Optional() @Inject(MAT_DIALOG_DATA) public dialogData: { museumId: number },
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
        debugger;
        if (this.formGroup.invalid) {
            return;
        }

        this.isFetching = true;

        const museum: Museum = {
            ...this.formGroup.getRawValue(),
            images: this.selectedImages,
            id: this.museumId,
        };

        const museumRequest: Observable<any> = this.museum
            ? this.museumService.update(museum)
            : this.museumService.create(museum);

        museumRequest.subscribe(() => {
            this.isFetching = false;

            if (this.isModal) {
                this.dialogRef.close(true);
            }

            this.resultMessageKey = 'museum.changed-successful';
            this.isInfoChanged = true;
        });
    }

    public onSelectFile(files: FileList) {
        if (files.length === 0)
            return;

        this.selectedImages = files;
    }

    private setMuseumId(): void {
        this.museumId = this.route.snapshot.params.id;

        if (this.isModal) {
            this.museumId = this.dialogData?.museumId;
        }
    }

    private fetchMuseum(): void {
        if (this.museumId) {
            this.isFetching = true;

            this.museumService.get(this.museumId)
                .pipe(
                    catchError(err => {
                        this.router.navigate(['museum']);
                        return throwError(err);
                    }),
                )
                .subscribe(data => {
                    this.museum = data;
                    this.formGroup.patchValue({
                        ...data,
                        countryId: data.country.id,
                        regionId: data.region.id,
                        cityId: data.city.id,
                        genreId: data.genre.id,
                    });

                    this.isFetching = false;
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
