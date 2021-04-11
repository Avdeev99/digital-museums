import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subject, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthUser } from 'src/app/core/auth/models/auth-user.model';
import { IOption } from 'src/app/core/form/form.interface';
import { CurrentUserService } from 'src/app/core/shared/services/current-user.service';
import { SouvenirDetails } from 'src/app/souvenir/models/souvenir-details.model';
import { Souvenir } from 'src/app/souvenir/models/souvenir.model';
import { SouvenirService } from 'src/app/souvenir/services/souvenir.service';
import { MuseumService } from 'src/app/museum/services/museum.service';

@Component({
  selector: 'app-souvenir-editing',
  templateUrl: './souvenir-editing.component.html',
  styleUrls: ['./souvenir-editing.component.scss']
})
export class SouvenirEditingComponent implements OnInit {
  public formGroup: FormGroup;
  public souvenir: SouvenirDetails;
  public museums$: Observable<Array<IOption>>;

  private souvenirId: number;
  private selectedImages: FileList;
  private currentUserId?: number;
  private unsubscribe$: Subject<void> = new Subject();

  public constructor(
      private fb: FormBuilder,
      private souvenirService: SouvenirService,
      private route: ActivatedRoute,
      private router: Router,
      private museumService: MuseumService,
      private currentUserService: CurrentUserService,
    ) {
    this.setSouvenirId();
    this.setUserId();
  }

  public ngOnInit(): void {
    this.initForm();
    this.initDropdowns();
    this.fetchSouvenir();
  }

  public ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  public onSubmit(): void {
    if (this.formGroup.invalid) {
    }

    const souvenir: Souvenir = {
      ...this.formGroup.getRawValue(),
      images: this.selectedImages,
      id: this.souvenirId,
    };

    const souvenirRequest: Observable<any> = this.souvenir
      ? this.souvenirService.update(souvenir)
      : this.souvenirService.create(souvenir);

      souvenirRequest.subscribe(() => this.router.navigate(['/']));
  }

  public onSelectFile(files: FileList) {
    if (files.length === 0)
        return;

    this.selectedImages = files;
  }

  private setSouvenirId(): void {
    this.souvenirId = this.route.snapshot.params.id;
  }

  private setUserId(): void {
    const currentUser: AuthUser = this.currentUserService.getUser();
    this.currentUserId = currentUser?.id;
  }

  private fetchSouvenir(): void {
    if (this.souvenirId) {
      this.souvenirService.get(this.souvenirId)
        .pipe(
          catchError(err => {
            this.router.navigate(['souvenir']);
            return throwError(err);
          }),
        )
        .subscribe(data => {
          this.souvenir = data;
          this.formGroup.patchValue(data);
        });
    }
  }

  private initForm(): void {
    this.formGroup = this.fb.group({
      museumId: new FormControl(null, [Validators.required]),
      name: new FormControl(null, [Validators.required]),
      description: new FormControl(null, [Validators.required]),
      price: new FormControl(null, [Validators.required]),
      availableUnits: new FormControl(null, [Validators.required]),
      tags: new FormControl([], [Validators.required]),
      images: new FormControl(null, [Validators.required]),
    });
  }

  private initDropdowns(): void {
    this.museums$ = this.museumService.getBaseListByUserId(this.currentUserId);
  }
}
