import { Component, OnInit, OnDestroy, AfterViewInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { IntervalObservable } from "rxjs/observable/IntervalObservable";

import { EnvironmentInfoModel } from './../models/environment-information.model';

import { IFarmService } from './../services/interface/farm-service.interface';
//This Component is stand for the card showing latest environemnt Data
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

    private intervalGetSensorDataSub: any;
    //constructor
    constructor(
        private activatedRoute: ActivatedRoute,
        private router: Router,
        private farmService: IFarmService
    ) {
        this.sub = this.activatedRoute.params.subscribe(params => {
            this.farmComponentId = +params['id'];
        });
    }

    //Function will be called after constructor
    ngOnInit() {
        //Get Lastes Environment Data to show with interval 5s
        this.intervalGetSensorDataSub =
            IntervalObservable.create(5000)
            .subscribe(() => {
                this.farmService.getEnvLatest(this.farmComponentId).then(
                    (data: EnvironmentInfoModel) => {
                        this.envLatestInfo = data;
                    },
                    (error: any) => {

                    }
                );
            }
        );
    }

    //Function will be called after user change to another page
    ngOnDestroy() {
        if (this.sub)
            this.sub.unsubscribe();
        if (this.intervalGetSensorDataSub)
            this.intervalGetSensorDataSub.unsubscribe();
    }

    ngAfterViewInit() {
    }
}