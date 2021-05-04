import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { CartService } from 'src/app/cart/services/cart.service';
import { MenuItem } from 'src/app/core/shared/models/menu-item.model';
import { SouvenirDetails } from '../../models/souvenir-details.model';
import { SouvenirService } from '../../services/souvenir.service';

@Component({
  selector: 'app-souvenir-details',
  templateUrl: './souvenir-details.component.html',
  styleUrls: ['./souvenir-details.component.scss']
})
export class SouvenirDetailsComponent implements OnInit {
  public souvenir: SouvenirDetails;

  public menuList: Array<MenuItem>;

  private backUrl: string;
  private souvenirId: number;
  private museumId: number;

  constructor(
    private route: ActivatedRoute,
    private souvenirService: SouvenirService,
    private router: Router,
    private cartService: CartService,
  ) { 
    this.setSouvenirId();
  }

  ngOnInit(): void {
    this.fetchSouvenir();
  }

  public get souvenirImage(): string {
    return this.souvenir && this.souvenir.imagePaths.length ? this.souvenir.imagePaths[0] : null;
  }

  private setSouvenirId(): void {
    this.souvenirId = this.route.snapshot.params.id;
  }

  public addToCart(): void {
    this.cartService.updateCartItem(this.souvenirId, 1).subscribe();
  }

  private fetchSouvenir(): void {
    this.souvenirService.get(this.souvenirId)
      .pipe(
        catchError(err => {
          this.router.navigate(['/']);
          return throwError(err);
        }),
      )
      .subscribe(data => {
        this.souvenir = data;
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
        href: `/museum/${this.museumId}`,
        state,
      },
      {
        name: 'menu.exhibits',
        href: `/exhibit/${this.museumId}/search`,
        state,
      },
      {
        name: 'menu.exhibitions',
        href: `/exhibition/${this.museumId}/search`,
        state,
      },
    ];
  }
}
