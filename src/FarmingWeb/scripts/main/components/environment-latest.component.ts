﻿import { Component, OnInit, OnDestroy, AfterViewInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { EnvironmentInfoModel } from './../models/environment-information.model';

import { IFarmService } from './../services/interface/farm-service.interface';

@Component({
    selector: 'env-latest',
    templateUrl: './templates/main/components/environment-latest.component.html'
})
export class EnvironmentLatestComponent implements OnInit, OnDestroy, AfterViewInit {
    public envLatestInfo: EnvironmentInfoModel = null;

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
        this.farmService.getEnvLatest(this.farmComponentId).then(
            (data: EnvironmentInfoModel) => {
                this.envLatestInfo = data;
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