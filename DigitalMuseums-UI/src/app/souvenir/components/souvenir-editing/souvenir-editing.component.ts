import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subject, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { IOption } from 'src/app/core/form/form.interface';
import { SouvenirDetails } from 'src/app/souvenir/models/souvenir-details.model';
import { Souvenir } from 'src/app/souvenir/models/souvenir.model';
import { SouvenirService } from 'src/app/souvenir/services/souvenir.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-souvenir-editing',
  templateUrl: './souvenir-editing.component.html',
  styleUrls: ['./souvenir-editing.component.scss']
})
export class SouvenirEditingComponent implements OnInit {
  public formGroup: FormGroup;
  public souvenir: SouvenirDetails;
  public museums$: Observable<Array<IOption>>;
  public isFetching: boolean = false;

  private souvenirId: number;
  private selectedImages: FileList;
  private unsubscribe$: Subject<void> = new Subject();

  public constructor(
      private fb: FormBuilder,
      private souvenirService: SouvenirService,
      private route: ActivatedRoute,
      private router: Router,
      private dialogRef: MatDialogRef<SouvenirEditingComponent>,
      @Inject(MAT_DIALOG_DATA) public dialogData: { souvenirId: number },
    ) {
    this.setSouvenirId();
  }

  public ngOnInit(): void {
    this.initForm();
    this.fetchSouvenir();
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

    const souvenir: Souvenir = {
      ...this.formGroup.getRawValue(),
      images: this.selectedImages,
      id: this.souvenirId,
    };

    const souvenirRequest: Observable<any> = this.souvenir
      ? this.souvenirService.update(souvenir)
      : this.souvenirService.create(souvenir);

    souvenirRequest.subscribe(() => {
      this.dialogRef.close(true);
      this.isFetching = false;
    });
  }

  public onSelectFile(files: FileList) {
    if (files.length === 0)
        return;

    this.selectedImages = files;
  }

  private setSouvenirId(): void {
    this.souvenirId = this.dialogData?.souvenirId;
  }

  private fetchSouvenir(): void {
    if (this.souvenirId) {
      this.isFetching = true;

      this.souvenirService.get(this.souvenirId)
        .pipe(
          catchError(err => {
            this.router.navigate(['souvenir']);
            this.isFetching = false;

            return throwError(err);
          }),
        )
        .subscribe(data => {
          this.souvenir = data;
          this.formGroup.patchValue(data);
          this.isFetching = false;
        });
    }
  }

  private initForm(): void {
    this.formGroup = this.fb.group({
      name: new FormControl(null, [Validators.required]),
      description: new FormControl(null, [Validators.required]),
      price: new FormControl(null, [Validators.required]),
      availableUnits: new FormControl(null, [Validators.required]),
      tags: new FormControl([], [Validators.required]),
      images: new FormControl(null, [Validators.required]),
    });
  }
}
