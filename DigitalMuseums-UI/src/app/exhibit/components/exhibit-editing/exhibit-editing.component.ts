import { HttpErrorResponse } from '@angular/common/http';
import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Subject, Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { IOption } from 'src/app/core/form/form.interface';
import { ExhibitDetails } from '../../models/exhibit-details.model';
import { Exhibit } from '../../models/exhibit.model';
import { ExhibitService } from '../../services/exhibit.service';


@Component({
  selector: 'app-exhibit-editing',
  templateUrl: './exhibit-editing.component.html',
  styleUrls: ['./exhibit-editing.component.scss']
})
export class ExhibitEditingComponent implements OnInit, OnDestroy {
  public formGroup: FormGroup;
  public exhibit: ExhibitDetails;
  public isFetching: boolean = false;
  public serverError: string;

  private exhibitId: number;
  private selectedImages: FileList;
  private unsubscribe$: Subject<void> = new Subject();

  public constructor(
      private fb: FormBuilder,
      private exhibitService: ExhibitService,
      private router: Router,
      private dialogRef: MatDialogRef<ExhibitEditingComponent>,
      @Inject(MAT_DIALOG_DATA) public dialogData: { exhibitId: number },
    ) {
    this.setExhibitId();
  }

  public ngOnInit(): void {
    this.initForm();
    this.fetchExhibit();
  }

  public ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  public onSubmit(): void {
    if (this.formGroup.invalid) {
      return;
    }

    this.isFetching = true;

    const exhibit: Exhibit = {
      ...this.formGroup.getRawValue(),
      images: this.selectedImages,
      id: this.exhibitId,
    };

    const exhibitRequest: Observable<any> = this.exhibit
      ? this.exhibitService.update(exhibit)
      : this.exhibitService.create(exhibit);

      exhibitRequest.pipe(
        catchError((errorResponse: HttpErrorResponse) => {
          this.isFetching = false;
          this.serverError = errorResponse.error.message;

          throw(errorResponse);
        })
      ).subscribe(() => {
        this.dialogRef.close(true);
        this.isFetching = false;
      });
  }

  public onSelectFile(files: FileList) {
    if (files.length === 0)
        return;

    this.selectedImages = files;
  }

  private setExhibitId(): void {
    this.exhibitId = this.dialogData?.exhibitId;
  }

  private fetchExhibit(): void {
    if (this.exhibitId) {
      this.isFetching = true;

      this.exhibitService.get(this.exhibitId)
        .pipe(
          catchError(err => {
            this.router.navigate(['exhibit']);
            return throwError(err);
          }),
        )
        .subscribe(data => {
          this.exhibit = data;
          this.formGroup.patchValue(data);

          this.isFetching = false;
        });
    }
  }

  private initForm(): void {
    this.formGroup = this.fb.group({
      name: new FormControl(null, [Validators.required]),
      description: new FormControl(null, [Validators.required]),
      author: new FormControl(null, [Validators.required]),
      date: new FormControl(null, [Validators.required]),
      tags: new FormControl([]),
      images: new FormControl(null),
    });

    if (!this.exhibitId) {
      this.formGroup.controls.images.setValidators(Validators.required);
    }
  }
}
