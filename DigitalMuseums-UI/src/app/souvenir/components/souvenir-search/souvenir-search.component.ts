import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { MenuItem } from 'src/app/core/shared/models/menu-item.model';
import { SouvenirDetails } from 'src/app/souvenir/models/souvenir-details.model';
import { SouvenirFilter } from 'src/app/souvenir/models/souvenir-filter.model';
import { SouvenirService } from 'src/app/souvenir/services/souvenir.service';

@Component({
  selector: 'app-souvenir-search',
  templateUrl: './souvenir-search.component.html',
  styleUrls: ['./souvenir-search.component.scss']
})
export class SouvenirSearchComponent implements OnInit {
  public formGroup: FormGroup;
  public souvenirs$: Observable<Array<SouvenirDetails>>
  public menuList: Array<MenuItem>;

  private backUrl: string;
  private museumId: number;

  constructor(
    private fb: FormBuilder,
    private souvenirService: SouvenirService,
    private route: ActivatedRoute,
    private router: Router,
  ) {
    this.setMuseumId();
    this.checkNavigationState();
  }

  ngOnInit(): void {
    this.initForm();
    this.souvenirs$ = this.souvenirService.getFiltered({
      museumId: this.museumId,
    });
  }

  public onSubmit(): void {
    const filter: SouvenirFilter = {
      ...this.formGroup.getRawValue(),
      museumId: this.museumId
    };
    
    this.souvenirs$ = this.souvenirService.getFiltered(filter);
  }

  public onDetails(souvenirId: number): void {
    this.router.navigate([`souvenir/${souvenirId}`]);
  }

  public getSouvenirImage(souvenir: SouvenirDetails): string {
    return souvenir ? 'url(' + souvenir.imagePath + ')'  : null;
  }

  private initForm(): void {
    this.formGroup = this.fb.group({
      name: new FormControl(null),
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
        name: 'menu.exhibits',
        href: `/exhibit/${this.museumId}/search`,
        state,
      },
    ];
  }
}
