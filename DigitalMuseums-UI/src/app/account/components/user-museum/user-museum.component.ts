import { Component, OnInit } from '@angular/core';
import { CurrentUserService } from 'src/app/core/shared/services/current-user.service';

@Component({
    selector: 'app-user-museum',
    templateUrl: './user-museum.component.html',
    styleUrls: ['./user-museum.component.scss']
})
export class UserMuseumComponent implements OnInit {
    navTabs: any[];

    constructor(private currentUserService: CurrentUserService) {
        this.initNavTabs();
    }

    ngOnInit(): void {
    }

    initNavTabs(): void {
        const user = this.currentUserService.getUser();

        this.navTabs = [
            {
                label: 'menu.museum',
                link: `./museum/update/${user.museumId}`,
                index: 0
            }, {
                label: 'menu.exhibits',
                link: `./exhibit/${user.museumId}/list`,
                index: 1
            }, 
            {
                label: 'menu.exhibitions',
                link: `./exhibition/${user.museumId}/list`,
                index: 2
            },
            {
                label: 'menu.souvenirs',
                link: `./souvenir/${user.museumId}/list`,
                index: 3
            },
        ];
    }
}
