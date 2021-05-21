import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthUser } from 'src/app/core/auth/models/auth-user.model';
import { IOption } from 'src/app/core/form/form.interface';
import { CurrentUserService } from 'src/app/core/shared/services/current-user.service';
import { MuseumService } from 'src/app/museum/services/museum.service';
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
  public museums$: Observable<Array<IOption>>;

  private exhibitId: number;
  private selectedImages: FileList;
  private currentUserId?: number;
  private unsubscribe$: Subject<void> = new Subject();

  public constructor(
      private fb: FormBuilder,
      private exhibitService: ExhibitService,
      private route: ActivatedRoute,
      private router: Router,
      private museumService: MuseumService,
      private currentUserService: CurrentUserService,
    ) {
    this.setExhibitId();
    this.setUserId();
  }

  public ngOnInit(): void {
    this.initForm();
    this.initDropdowns();
    this.fetchExhibit();
  }

  public ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  public onSubmit(): void {
    if (this.formGroup.invalid) {
    }

    const exhibit: Exhibit = {
      ...this.formGroup.getRawValue(),
      images: this.selectedImages,
      id: this.exhibitId,
    };

    const exhibitRequest: Observable<any> = this.exhibit
      ? this.exhibitService.update(exhibit)
      : this.exhibitService.create(exhibit);

      exhibitRequest.subscribe(() => this.router.navigate(['exhibit']));
  }

  public onSelectFile(files: FileList) {
    if (files.length === 0)
        return;

    this.selectedImages = files;
  }

  private setExhibitId(): void {
    this.exhibitId = this.route.snapshot.params.id;
  }

  private setUserId(): void {
    const currentUser: AuthUser = this.currentUserService.getUserData();
    this.currentUserId = currentUser?.id;
  }

  private fetchExhibit(): void {
    if (this.exhibitId) {
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
        });
    }
  }

  private initForm(): void {
    this.formGroup = this.fb.group({
      museumId: new FormControl(null, [Validators.required]),
      name: new FormControl(null, [Validators.required]),
      description: new FormControl(null, [Validators.required]),
      author: new FormControl(null, [Validators.required]),
      date: new FormControl(null, [Validators.required]),
      tags: new FormControl([], [Validators.required]),
      images: new FormControl(null, [Validators.required]),
    });
  }

  private initDropdowns(): void {
    this.museums$ = this.museumService.getBaseListByUserId(this.currentUserId);
  }
}
