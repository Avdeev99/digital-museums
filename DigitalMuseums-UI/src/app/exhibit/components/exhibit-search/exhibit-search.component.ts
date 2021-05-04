import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { MenuItem } from 'src/app/core/shared/models/menu-item.model';
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
  public menuList: Array<MenuItem>;

  private backUrl: string;
  private museumId: number;

  constructor(
    private fb: FormBuilder,
    private exhibitService: ExhibitService,
    private route: ActivatedRoute,
    private router: Router,
  ) {
    this.setMuseumId();
    this.checkNavigationState()
  }

  ngOnInit(): void {
    this.initForm();
    this.exhibits$ = this.exhibitService.getFiltered({
      museumId: this.museumId,
    });
  }

  public onSubmit(): void {
    const filter: ExhibitFilter = {
      ...this.formGroup.getRawValue(),
      museumId: this.museumId
    };
    
    this.exhibits$ = this.exhibitService.getFiltered(filter);
  }

  public onDetails(exhibitId: number): void {
    this.router.navigate([`exhibit/${exhibitId}`]);
  }

  public getExhibitImage(exhibit: ExhibitDetails): string {
    return exhibit ? 'url(' + exhibit.imagePath + ')'  : null;
  }

  private initForm(): void {
    this.formGroup = this.fb.group({
      name: new FormControl(null),
      author: new FormControl(null),
      date: new FormControl(null),
      tags: new FormControl([]),
    });
  }

  private setMuseumId(): void {
    this.museumId = this.route.snapshot.params.museumId;
  }

  private checkNavigationState(): void {
    const currentNavigationState: any = this.router.getCurrentNavigation();

    if (currentNavigationState && currentNavigationState.extras && currentNavigationState.extras.state) {
      this.backUrl = currentNavigationState.extras.state.backUrl;
    }

    this.initMenuList();
  }

  private initMenuList(): void {
    const state: { backUrl?: string } = this.backUrl ? { backUrl: this.backUrl } : {};
    this.menuList = [
      {
        name: 'menu.museum',
        href: `/museum/${this.museumId}`,
        state,
      },
      {
        name: 'menu.souvenirs',
        href: `/souvenir/${this.museumId}/search`,
        state,
      },
    ];
  }
}
