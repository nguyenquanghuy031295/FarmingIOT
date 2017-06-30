import { Component, OnInit, AfterViewInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { EnvironmentInfoModel } from './../models/environment-information.model';

import { IFarmService } from './../services/interface/farm-service.interface';

@Component({
    selector: 'env-chart',
    templateUrl: './templates/main/components/env-chart.component.html'
})
export class EnvChartComponent implements OnInit, OnDestroy, AfterViewInit {
    private data: any;
    private sub: any;
    private farmComponentId: number = 0;
    private envData: EnvironmentInfoModel[] = null;

    private labels: string[] = [];
    private dataTemp: any;
    private dataLum: any;
    private dataSoilHum: any;

    constructor(
        private activatedRoute: ActivatedRoute,
        private router: Router,
        private farmService: IFarmService
    ) {
        this.sub = this.activatedRoute.params.subscribe(params => {
            this.farmComponentId = +params['id'];
        });

        this.data = {
            labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
            datasets: [
                {
                    label: 'First Dataset',
                    data: [65, 59, 80, 81, 56, 55, 40],
                    fill: false,
                    borderColor: '#4bc0c0'
                },
                {
                    label: 'Second Dataset',
                    data: [28, 48, 40, 19, 86, 27, 90],
                    fill: false,
                    borderColor: '#565656'
                }
            ]
        }
    }

    ngOnInit() {
        this.farmService.getEnvInfoToday(this.farmComponentId).then(
            (data: EnvironmentInfoModel[]) => {
                this.envData = data;
                this.processData();
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

    processData() {
        this.getLabelsX();
        this.processDataForTempChart();
        this.processDataForLumChart();
        this.processDataForSoilChart();
    }

    getLabelsX() {
        this.labels = this.envData.map((envInfo: EnvironmentInfoModel) => {
            return envInfo.Timestamp.toLocaleString().slice(11);
        });
    }

    processDataForTempChart() {
        let data: number[] = this.envData.map((envInfo: EnvironmentInfoModel) => {
            return envInfo.Temperature;
        });
        this.dataTemp = {
            labels: this.labels,
            datasets: [
                {
                    label: 'Temperature',
                    data: data,
                    fill: false,
                    borderColor: '#f42a04'
                }
            ]
        }
    }

    processDataForLumChart() {
        let data: number[] = this.envData.map((envInfo: EnvironmentInfoModel) => {
            return envInfo.Luminosity;
        });
        this.dataLum = {
            labels: this.labels,
            datasets: [
                {
                    label: 'Luminosity',
                    data: data,
                    fill: false,
                    borderColor: '#565656'
                }
            ]
        }
    }

    processDataForSoilChart() {
        let data: number[] = this.envData.map((envInfo: EnvironmentInfoModel) => {
            return envInfo.Soil_Humidity;
        });
        this.dataSoilHum = {
            labels: this.labels,
            datasets: [
                {
                    label: 'Soil Humidity',
                    data: data,
                    fill: false,
                    borderColor: '#4bc0c0'
                }
            ]
        }
    }
}