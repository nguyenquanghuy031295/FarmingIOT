import { Component } from '@angular/core';
import { Router } from '@angular/router';


@Component({
    selector: 'not-found',
    templateUrl: './templates/main/components/page-not-found.component.html',
    styleUrls: ['css/404.css']
})
export class PageNotFoundComponent {
    constructor(
        private router: Router
    ) {
    }
}