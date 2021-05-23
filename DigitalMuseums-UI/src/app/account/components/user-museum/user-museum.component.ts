import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthUser } from 'src/app/core/auth/models/auth-user.model';
import { CurrentUserService } from 'src/app/core/shared/services/current-user.service';

@Component({
    selector: 'app-user-museum',
    templateUrl: './user-museum.component.html',
    styleUrls: ['./user-museum.component.scss']
})
export class UserMuseumComponent implements OnInit {
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
        const isBaseComponentUrl = this.router.url === '/account/museum';
        
        if (isBaseComponentUrl) {
            this.router.navigate([`account/museum/update/${this.user.museumId}`]);
        }
    }

    initNavTabs(): void {
        this.navTabs = [
            {
                label: 'menu.museum',
                link: `./museum/update/${this.user.museumId}`,
                index: 0
            }, {
                label: 'menu.exhibits',
                link: `./exhibit/${this.user.museumId}/list`,
                index: 1
            }, 
            {
                label: 'menu.exhibitions',
                link: `./exhibition/${this.user.museumId}/list`,
                index: 2
            },
            {
                label: 'menu.souvenirs',
                link: `./souvenir/${this.user.museumId}/list`,
                index: 3
            },
        ];
    }
}
