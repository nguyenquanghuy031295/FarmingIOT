import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';

//This Component is stand for the card show environment data
@Component({
    selector: 'env-card',
    templateUrl: './templates/main/components/env-card.component.html',
    styleUrls: ['css/w3-farming.css']
})
export class EnvCardComponent {
    @Input() title: string = "";
    @Input() data: number | string;
    @Input() symbol: string = "";
    @Input() srcImg: string = "../../../images/default.png";
    //constructor
    constructor(
        private router: Router
    ) {
    }

    //check value is number or not
    isNumber(val: any) {
        return typeof val === 'number';
    }

    //check value is string or not
    isString(val: any) {
        return typeof val === 'string';
    }
}