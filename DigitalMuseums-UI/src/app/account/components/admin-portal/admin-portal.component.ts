import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthUser } from 'src/app/core/auth/models/auth-user.model';
import { CurrentUserService } from 'src/app/core/shared/services/current-user.service';

@Component({
  selector: 'app-admin-portal',
  templateUrl: './admin-portal.component.html',
  styleUrls: ['./admin-portal.component.scss']
})
export class AdminPortalComponent implements OnInit {
  navTabs: any[];
  user: AuthUser;

  constructor(
      private currentUserService: CurrentUserService,
      private router: Router,
  ) {
      this.user = this.currentUserService.getUserData();
      this.initNavTabs();
  }

  ngOnInit(): void {
      const isBaseComponentUrl = this.router.url === '/account/admin-portal';
      
      if (isBaseComponentUrl) {
          this.router.navigate(['account/admin-portal/museum/list']);
      }
  }

  initNavTabs(): void {
      this.navTabs = [
          {
              label: 'menu.museums',
              link: `./museum/list`,
              index: 0
          }, {
              label: 'menu.profile.genres',
              link: `./genres`,
              index: 1
          }, 
      ];
  }
}
