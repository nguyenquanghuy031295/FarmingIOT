import { Component } from '@angular/core';
//This Component is for construct the structure of web
@Component({
    selector: 'farm-main',
    template: `
    <app-menu></app-menu>
    <router-outlet></router-outlet>
`
})
export class MainComponent { }