import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { IAuthenticateService } from './../services/interface/authenticate.-service.interface';

@Component({
    selector: 'app-menu',
    templateUrl: './templates/main/components/app-menu.component.html',
    styleUrls: ['css/placespot.css']
})
export class AppMenuComponent {
    constructor(
        private router: Router,
        private authenticateService: IAuthenticateService
    ) { }

    navigateToProfile() {
        this.router.navigate(['farmiot/profile']);
    }

    navigateToNewFarm() {
        this.router.navigate(['farmiot/newfarm']);
    }

    navigateToMain() {
        this.router.navigate(['farmiot/main']);
    }

    OnSignOut() {
        this.authenticateService.logout().then(
            () => {
                this.router.navigate(['signin']);
            }, () => {

            }
        );
    }
}