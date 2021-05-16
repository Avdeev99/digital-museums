import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/core/auth/auth.service';
import { MenuItem } from 'src/app/core/shared/models/menu-item.model';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
  public menuList: Array<MenuItem>;

  public constructor(private authService: AuthService) {}

  public ngOnInit(): void {
    this.initMenulist();
  }

  public get isAuthenticated(): boolean {
    return this.authService.isAuthenticated();
  }

  private initMenulist(): void {
    this.menuList = [
      {
        name: 'menu.profile.change-password',
        href: '/account/change-password',
      },
      {
        name: 'menu.profile.logout',
        href: `/`,
      },
    ];
  }
}
