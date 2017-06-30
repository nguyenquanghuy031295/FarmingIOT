import { Component } from '@angular/core';
import { Router } from '@angular/router';
@Component({
    selector: 'app-menu',
    templateUrl: './templates/main/components/app-menu.component.html',
    styleUrls: ['css/placespot.css']
})
export class AppMenuComponent {
    constructor(
        private router: Router
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
}