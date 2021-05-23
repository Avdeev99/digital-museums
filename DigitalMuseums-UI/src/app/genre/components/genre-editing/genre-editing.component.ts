import { HttpErrorResponse } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Observable, Subject, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { IOption } from 'src/app/core/form/form.interface';
import { GenreService } from 'src/app/core/shared/services/genre.service';
import { GenreDetails } from '../../models/genre-details.model';

@Component({
  selector: 'app-genre-editing',
  templateUrl: './genre-editing.component.html',
  styleUrls: ['./genre-editing.component.scss']
})
export class GenreEditingComponent implements OnInit {
  public formGroup: FormGroup;
  public genre: GenreDetails;
  public isFetching: boolean = false;
  public serverError: string;

  private genreId: number;
  private unsubscribe$: Subject<void> = new Subject();

  public constructor(
      private fb: FormBuilder,
      private genreService: GenreService,
      private router: Router,
      private dialogRef: MatDialogRef<GenreEditingComponent>,
      @Inject(MAT_DIALOG_DATA) public dialogData: { genreId: number },
    ) {
    this.setGenreId();
  }

  public ngOnInit(): void {
    this.initForm();
    this.fetchGenre();
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

    const genre: GenreDetails = {
      ...this.formGroup.getRawValue(),
      id: this.genreId,
    };

    const genreRequest: Observable<any> = this.genre
      ? this.genreService.update(genre)
      : this.genreService.create(genre);

      genreRequest.pipe(
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

  private setGenreId(): void {
    this.genreId = this.dialogData?.genreId;
  }

  private fetchGenre(): void {
    if (this.genreId) {
      this.isFetching = true;

      this.genreService.get(this.genreId)
        .pipe(
          catchError(err => {
            this.router.navigate(['genre']);
            return throwError(err);
          }),
        )
        .subscribe(data => {
          this.genre = data;
          this.formGroup.patchValue(data);

          this.isFetching = false;
        });
    }
  }

  private initForm(): void {
    this.formGroup = this.fb.group({
      name: new FormControl(null, [Validators.required]),
    });
  }
}
