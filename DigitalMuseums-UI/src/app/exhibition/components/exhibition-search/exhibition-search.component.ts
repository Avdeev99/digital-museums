import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { MenuItem } from 'src/app/core/shared/models/menu-item.model';
import { Exhibition, ExhibitionFilter } from '../../models/exhibition.model';
import { ExhibitionService } from '../../services/exhibition.service';

@Component({
  selector: 'app-exhibition-search',
  templateUrl: './exhibition-search.component.html',
  styleUrls: ['./exhibition-search.component.scss']
})
export class ExhibitionSearchComponent implements OnInit {
  public formGroup: FormGroup;
  public exhibitions$: Observable<Array<Exhibition>>
  public menuList: Array<MenuItem>;

  private backUrl: string;
  private museumId: number;

  constructor(
    private fb: FormBuilder,
    private exhibitionService: ExhibitionService,
    private route: ActivatedRoute,
    private router: Router,
  ) {
    this.setMuseumId();
    this.checkNavigationState()
  }

  ngOnInit(): void {
    this.initForm();
    this.exhibitions$ = this.exhibitionService.getFiltered({
      museumId: this.museumId,
    });
  }

  public onSubmit(): void {
    const filter: ExhibitionFilter = {
      ...this.formGroup.getRawValue(),
      museumId: this.museumId
    };
    
    this.exhibitions$ = this.exhibitionService.getFiltered(filter);
  }

  public onDetails(exhibitionId: number): void {
    this.router.navigate([`exhibition/${exhibitionId}`]);
  }

  public getExhibitionImage(exhibition: Exhibition): string {
    return exhibition ? 'url(' + exhibition.imagePath + ')'  : null;
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
      {
        name: 'menu.souvenirs',
        href: `/souvenir/${this.museumId}/search`,
        state,
      },
    ];
  }
}
