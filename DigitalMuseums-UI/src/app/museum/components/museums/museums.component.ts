import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { GenreService } from 'src/app/core/shared/services/genre.service';
import { LocationService } from 'src/app/core/shared/services/location.service';
import { MuseumBase } from '../../models/museum-base';
import { MuseumDetails } from '../../models/museum-details.model';
import { MuseumFilter } from '../../models/museum-filter.model';
import { MuseumService } from '../../services/museum.service';

@Component({
  selector: 'app-museums',
  templateUrl: './museums.component.html',
  styleUrls: ['./museums.component.scss']
})
export class MuseumsComponent extends MuseumBase implements OnInit {
  public formGroup: FormGroup;
  public museums$: Observable<Array<MuseumDetails>>

  public isFetching: boolean = false;

  constructor(
    private fb: FormBuilder,
    private museumService: MuseumService,
    protected locationService: LocationService,
    protected genreService: GenreService,
    private router: Router,
  ) {
    super(locationService, genreService);
  }

  ngOnInit(): void {
    this.isFetching = true;

    this.initForm();
    this.initSubscriptions();
    this.museums$ = this.museumService.getAll();

    this.museums$.subscribe(() => {
      this.isFetching = false;
    });
  }

  public getMuseumImage(museum: MuseumDetails): string {
    return museum ? 'url(' + museum.imagePath + ')'  : null;
  }

  public onSubmit(): void {
    const filter: MuseumFilter = this.formGroup.getRawValue();

    this.isFetching = true;

    this.museums$ = this.museumService.getFiltered(filter);

    this.museums$.subscribe(() => {
      this.isFetching = false;
    });
  }

  public onDetails(museumId: number): void {
    this.router.navigate([`museum/${museumId}`]);
  }

  private initForm(): void {
    this.formGroup = this.fb.group({
      name: new FormControl(null),
      countryId: new FormControl(null),
      regionId: new FormControl(null),
      cityId: new FormControl(null),
      genres: new FormControl(null),
      sortingMethod: new FormControl(null)
    });
  }

  private initSubscriptions(): void {
    this.countryValueChanges(this.formGroup.controls.countryId);
    this.regionValueChanges(this.formGroup.controls.regionId);
  }
}
