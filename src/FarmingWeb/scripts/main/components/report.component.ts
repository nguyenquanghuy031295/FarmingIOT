import { Component, OnInit, OnDestroy, AfterViewInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { EnvironmentInfoModel } from './../models/environment-information.model';

import { IDeviceService } from './../services/interface/device-service.interface';

//This component is stand for Report Page
@Component({
    selector: 'report',
    templateUrl: './templates/main/components/report.component.html'
})
export class ReportComponent implements OnInit, OnDestroy, AfterViewInit {
    private farmComponentId: number = 0;
    private sub: any;

    public sensorData: EnvironmentInfoModel[] = [];


    //constructor
    constructor(
        private activatedRoute: ActivatedRoute,
        private router: Router,
        private deviceService: IDeviceService,
    ) {
        this.sub = this.activatedRoute.params.subscribe(params => {
            this.farmComponentId = +params['id'];
        });
    }

    //Function will be called after constructor
    ngOnInit() {
        //Get all environemnt data
        this.deviceService.GetSensorData(this.farmComponentId).then(
            (data: EnvironmentInfoModel[]) => {
                this.sensorData = data;
            },
            (error: any) => {

            }
        );
    }

    //Function will be called when user change to another page
    ngOnDestroy() {
        if (this.sub)
            this.sub.unsubscribe();
    }

    ngAfterViewInit() {
    }
}