import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';


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
    constructor(
        private router: Router
    ) {
    }

    isNumber(val: any) {
        return typeof val === 'number';
    }

    isString(val: any) {
        return typeof val === 'string';
    }
}