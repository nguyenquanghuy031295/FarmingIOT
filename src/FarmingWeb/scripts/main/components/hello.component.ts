import { Component } from '@angular/core';
import { Router } from '@angular/router';


@Component({
    selector: 'main',
    template: `<h1>Hello {{name}}</h1>`
})
export class HelloComponent {
    name = 'you';
    constructor(
        private router: Router
    ) {
    }

}