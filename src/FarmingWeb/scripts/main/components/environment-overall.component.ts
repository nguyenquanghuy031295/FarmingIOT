import { Component, OnInit, OnDestroy, AfterViewInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { EnvOverallInfoModel } from './../models/env-overall-info.model';

import { IFarmService } from './../services/interface/farm-service.interface';

@Component({
    selector: 'env-overall',
    templateUrl: './templates/main/components/environment-overall.component.html'
})
export class EnvironmentOverallComponent implements OnInit, OnDestroy, AfterViewInit {
    public envOverallInfo: EnvOverallInfoModel = null;

    private imgTemp: string = "../../../images/temperature.png";
    private imgLum: string = "../../../images/lamp.png";
    private imgSoil: string = "../../../images/liquid.png";
    private farmComponentId: number = 0;
    private sub: any;

    constructor(
        private activatedRoute: ActivatedRoute,
        private router: Router,
        private farmService: IFarmService
    ) {
        this.sub = this.activatedRoute.params.subscribe(params => {
            this.farmComponentId = +params['id'];
        });
    }
    ngOnInit() {
        this.farmService.getEnvOverallMonth(this.farmComponentId).then(
            (data: EnvOverallInfoModel) => {
                this.envOverallInfo = data;
            },
            (error: any) => {

            }
        );
    }

    ngOnDestroy() {
        if (this.sub)
            this.sub.unsubscribe();
    }

    ngAfterViewInit() {
    }
}