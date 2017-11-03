import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { IAuthenticateService } from './../services/interface/authenticate.-service.interface';

//This Component is stand for the menu header of app
@Component({
    selector: 'app-menu',
    templateUrl: './templates/main/components/app-menu.component.html',
    styleUrls: ['css/placespot.css']
})
export class AppMenuComponent {
    //constructor
    constructor(
        private router: Router,
        private authenticateService: IAuthenticateService
    ) { }

    //navigate to Profile of User
    navigateToProfile() {
        this.router.navigate(['farmiot/profile']);
    }

    //Navigate to Page for adding new Farm
    navigateToNewFarm() {
        this.router.navigate(['farmiot/newfarm']);
    }

    //Navigate to Main Page
    navigateToMain() {
        this.router.navigate(['farmiot/main']);
    }

    //Sign out
    OnSignOut() {
        this.authenticateService.logout().then(
            () => {
                this.router.navigate(['signin']);
            }, () => {

            }
        );
    }
}