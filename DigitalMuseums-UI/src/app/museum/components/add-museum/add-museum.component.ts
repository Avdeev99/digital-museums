import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { Observable, Subject } from 'rxjs';
import { LocationService } from 'src/app/core/shared/services/location.service';
import { IOption } from 'src/app/core/form/form.interface';

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

  public countries: Array<IOption>;

  public constructor(
    private fb: FormBuilder,
    private locationService: LocationService,
    ) {}

  private unsubscribe$: Subject<void> = new Subject();

  public ngOnInit(): void {
    this.initForm();

    this.locationService.getCountries().subscribe(data => {
      console.log(data);
      this.countries = data;
    });
  }

  public ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  public onSubmit(): void {

  }

  private initForm(): void {
    this.formGroup = this.fb.group({
      name: new FormControl(''),
      description: new FormControl(''),
      countryId: new FormControl(''),
      regionId: new FormControl(''),
      cityId: new FormControl(''),
      address: new FormControl(''),
      genreId: new FormControl(''),
      images: new FormControl(''),
    });
  }
}
