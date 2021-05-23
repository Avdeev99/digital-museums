import { ExhibitService } from 'src/app/exhibit/services/exhibit.service';
import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable, Subject, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthUser } from 'src/app/core/auth/models/auth-user.model';
import { IOption, IOptionChecked } from 'src/app/core/form/form.interface';
import { CurrentUserService } from 'src/app/core/shared/services/current-user.service';
import { Exhibition, ExhibitionEditing } from 'src/app/exhibition/models/exhibition.model';
import { ExhibitionService } from 'src/app/exhibition/services/exhibition.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-exhibition-editing',
  templateUrl: './exhibition-editing.component.html',
  styleUrls: ['./exhibition-editing.component.scss']
})
export class ExhibitionEditingComponent implements OnInit {
  public formGroup: FormGroup;
  public exhibition: Exhibition;
  public museums$: Observable<Array<IOption>>;
  public exhibitOptions: Array<IOptionChecked>;
  public selectedExhibits: Array<IOptionChecked>;

  private exhibitionId: number;
  private selectedImages: FileList;
  public isFetching: boolean = false;
  public serverError: string;

  private unsubscribe$: Subject<void> = new Subject();

  public constructor(
    private fb: FormBuilder,
    private exhibitionService: ExhibitionService,
    private exhibitService: ExhibitService,
    private router: Router,
    private currentUserService: CurrentUserService,
    private dialogRef: MatDialogRef<ExhibitionEditingComponent>,
    @Inject(MAT_DIALOG_DATA) public dialogData: { exhibitionId: number },
  ) {
    this.setExhibitionId();
  }

  public ngOnInit(): void {
    this.initForm();
    this.fetchExhibition();
    this.initExhibits();
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

    const exhibition: ExhibitionEditing = {
      ...this.formGroup.getRawValue(),
      id: this.exhibitionId,
      images: this.selectedImages,
    };

    const exhibitionRequest: Observable<any> = this.exhibition
      ? this.exhibitionService.update(exhibition)
      : this.exhibitionService.create(exhibition);

    exhibitionRequest.pipe(
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

  private setExhibitionId(): void {
    this.exhibitionId = this.dialogData?.exhibitionId;
  }

  private fetchExhibition(): void {
    if (this.exhibitionId) {
      this.isFetching = true;

      this.exhibitionService.get(this.exhibitionId)
        .pipe(
          catchError(err => {
            this.router.navigate(['exhibition']);
            return throwError(err);
          }),
        )
        .subscribe(data => {
          this.exhibition = data;
          this.formGroup.patchValue(data);

          this.isFetching = false;
        });
    }
  }

  private initForm(): void {
    this.formGroup = this.fb.group({
      name: new FormControl(null, [Validators.required]),
      description: new FormControl(null, [Validators.required]),
      tags: new FormControl([]),
      exhibitIds: new FormControl(null, [Validators.required]),
      images: new FormControl(null, [Validators.required]),
    });
  }

  private initExhibits(): void {
    const currentUser: AuthUser = this.currentUserService.getUserData();
    const museumId: number = currentUser?.museumId;

    this.isFetching = true;

    this.exhibitService.getFiltered({
      museumId: museumId
    }).subscribe(exhibits => {
      this.exhibitOptions = exhibits.map((exhibit) => {
        const exhibitSelected: boolean = !!this.exhibition?.exhibits?.find(e => e.id === exhibit.id);

        return { 
          id: exhibit.id,
          name: exhibit.name,
          selected: exhibitSelected,
        }
      });

      const selectedExhibitIds = this.exhibitOptions.filter(e => e.selected).map(e => e.id);

      setTimeout(() => {
        this.formGroup.controls.exhibitIds.setValue(selectedExhibitIds);
        this.isFetching = false;
      }, 0);
    })
  }
}
