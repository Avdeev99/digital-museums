import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { ExhibitDetails } from '../../models/exhibit-details.model';
import { ExhibitFilter } from '../../models/exhibit-filter.model';
import { ExhibitService } from '../../services/exhibit.service';

@Component({
  selector: 'app-exhibit-search',
  templateUrl: './exhibit-search.component.html',
  styleUrls: ['./exhibit-search.component.scss']
})
export class ExhibitSearchComponent implements OnInit {
  public formGroup: FormGroup;
  public exhibits$: Observable<Array<ExhibitDetails>>

  private museumId: number;

  constructor(
    private fb: FormBuilder,
    private exhibitService: ExhibitService,
    private route: ActivatedRoute,
    private router: Router,
  ) {
    this.setMuseumId();
  }

  ngOnInit(): void {
    this.initForm();
    this.exhibits$ = this.exhibitService.getAll();
  }

  public onSubmit(): void {
    const filter: ExhibitFilter = {
      ...this.formGroup.getRawValue(),
      museumId: this.museumId
    };
    
    this.exhibits$ = this.exhibitService.getFiltered(filter);
  }

  private initForm(): void {
    this.formGroup = this.fb.group({
      name: new FormControl(null),
      author: new FormControl(null),
      date: new FormControl(null),
      tags: new FormControl(null),
    });
  }

  private setMuseumId(): void {
    this.museumId = this.route.snapshot.params.museumId;
  }
}
