import { Component, OnInit, OnDestroy, AfterViewInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { EnvironmentInfoModel } from './../models/environment-information.model';

import { IDeviceService } from './../services/interface/device-service.interface';
@Component({
    selector: 'report',
    templateUrl: './templates/main/components/report.component.html'
})
export class ReportComponent implements OnInit, OnDestroy, AfterViewInit {
    private farmComponentId: number = 0;
    private sub: any;

    public sensorData: EnvironmentInfoModel[] = [];

    constructor(
        private activatedRoute: ActivatedRoute,
        private router: Router,
        private deviceService: IDeviceService,
    ) {
        this.sub = this.activatedRoute.params.subscribe(params => {
            this.farmComponentId = +params['id'];
        });
    }

    ngOnInit() {
        this.deviceService.GetSensorData(this.farmComponentId).then(
            (data: EnvironmentInfoModel[]) => {
                this.sensorData = data;
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