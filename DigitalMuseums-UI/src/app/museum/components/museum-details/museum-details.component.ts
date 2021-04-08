import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { MenuItem } from 'src/app/core/shared/models/menu-item.model';
import { MuseumDetails } from '../../models/museum-details.model';
import { MuseumService } from '../../services/museum.service';

@Component({
  selector: 'app-museum-details',
  templateUrl: './museum-details.component.html',
  styleUrls: ['./museum-details.component.scss']
})
export class MuseumDetailsComponent implements OnInit {
  public museum: MuseumDetails;
  public menuList: Array<MenuItem>;

  private museumId: number;
  private backUrl: string;

  constructor(
    private route: ActivatedRoute,
    private museumService: MuseumService,
    private router: Router,
  ) { 
    this.setMuseumId();
    this.checkNavigationState();
  }

  ngOnInit(): void {
    this.fetchMuseum();
  }

  public get museumImage(): string {
    return this.museum && this.museum.imagePaths.length ? this.museum.imagePaths[0] : null;
  }

  private setMuseumId(): void {
    this.museumId = this.route.snapshot.params.id;
  }

  private fetchMuseum(): void {
    this.museumService.get(this.museumId)
      .pipe(
        catchError(err => {
          this.router.navigate(['museum']);
          return throwError(err);
        }),
      )
      .subscribe(data => {
        this.museum = data;
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
    let state: any = this.backUrl ? { backUrl: this.backUrl } : {};

    this.menuList = [
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
