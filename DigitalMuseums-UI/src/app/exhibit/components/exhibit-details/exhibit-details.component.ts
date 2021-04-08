import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { MenuItem } from 'src/app/core/shared/models/menu-item.model';
import { ExhibitDetails } from '../../models/exhibit-details.model';
import { ExhibitService } from '../../services/exhibit.service';

@Component({
  selector: 'app-exhibit-details',
  templateUrl: './exhibit-details.component.html',
  styleUrls: ['./exhibit-details.component.scss']
})
export class ExhibitDetailsComponent implements OnInit {
  public exhibit: ExhibitDetails;

  public menuList: Array<MenuItem>;

  private backUrl: string;
  private exhibitId: number;
  private museumId: number;

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

  public get exhibitImage(): string {
    return this.exhibit && this.exhibit.imagePaths.length ? this.exhibit.imagePaths[0] : null;
  }

  private setExhibitId(): void {
    this.exhibitId = this.route.snapshot.params.id;
  }

  private fetchExhibit(): void {
    this.exhibitService.get(this.exhibitId)
      .pipe(
        catchError(err => {
          this.router.navigate(['exhibit']);
          return throwError(err);
        }),
      )
      .subscribe(data => {
        this.exhibit = data;
        this.museumId = data.museumId;

        this.checkNavigationState();
      });
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
        href: `museum/${this.museumId}`,
        state,
      },
    ];
  }
}
