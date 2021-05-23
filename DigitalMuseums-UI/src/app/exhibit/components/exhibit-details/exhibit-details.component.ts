import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { MenuItem } from 'src/app/core/shared/models/menu-item.model';
import { RelatedItem } from 'src/app/core/shared/models/related-item.model';
import { ExhibitDetails } from '../../models/exhibit-details.model';
import { ExhibitService } from '../../services/exhibit.service';

declare var carousel: any;

@Component({
  selector: 'app-exhibit-details',
  templateUrl: './exhibit-details.component.html',
  styleUrls: ['./exhibit-details.component.scss']
})
export class ExhibitDetailsComponent implements OnInit {
  public exhibit: ExhibitDetails;

  public menuList: Array<MenuItem>;
  public relatedItems: Array<RelatedItem>;

  private backUrl: string;
  private exhibitId: number;
  private museumId: number;

  private maxRelatedExhibitionsOnPage: number = 5;

  constructor(
    private route: ActivatedRoute,
    private exhibitService: ExhibitService,
    private router: Router,
  ) { 
    this.setExhibitId();
  }

  ngOnInit(): void {
    this.fetchExhibit();
  }

  public get exhibitImages(): string[] {
    return this.exhibit && this.exhibit.imagePaths.length ? this.exhibit.imagePaths: null;
  }

  private setExhibitId(): void {
    this.exhibitId = this.route.snapshot.params.id;
  }

  private fetchExhibit(): void {
    this.exhibitService.get(this.exhibitId)
      .pipe(
        catchError(err => {
          this.router.navigate(['/']);
          return throwError(err);
        }),
      )
      .subscribe(data => {
        this.exhibit = data;
        this.museumId = data.museumId;

        this.checkNavigationState();
        carousel();
      });
  }

  private checkNavigationState(): void {
    const currentNavigationState: any = this.router.getCurrentNavigation();

    if (currentNavigationState && currentNavigationState.extras && currentNavigationState.extras.state) {
      this.backUrl = currentNavigationState.extras.state.backUrl;
    }

    this.initMenuList();
    this.initRelatedItems();
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
        name: 'menu.exhibitions',
        href: `/exhibition/${this.museumId}/search`,
        state,
      },
      {
        name: 'menu.souvenirs',
        href: `/souvenir/${this.museumId}/search`,
        state,
      },
    ];
  }

  private initRelatedItems(): void {
    const itemsCount = Math.min(this.exhibit.exhibitions.length, this.maxRelatedExhibitionsOnPage);
    const relatedItems: RelatedItem[] = this.exhibit.exhibitions.map(x => ({name: x.name, href: `/exhibition/${x.id}`})).slice(0, itemsCount);
    this.relatedItems = relatedItems;
  }
}
