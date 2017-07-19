import { Component, OnInit, AfterViewInit, OnDestroy, ViewChildren, QueryList } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UIChart } from 'primeng/primeng'

import { EnvironmentInfoModel } from './../models/environment-information.model';

import { IFarmService } from './../services/interface/farm-service.interface';

@Component({
    selector: 'env-chart',
    templateUrl: './templates/main/components/env-chart.component.html'
})
export class EnvChartComponent implements OnInit, OnDestroy, AfterViewInit {
    private sub: any;
    private farmComponentId: number = 0;
    private envData: EnvironmentInfoModel[] = null;
    private choosenDate: Date = new Date();
    private dateFormat: string = "dd/mm/yy"
    private labels: string[] = [];
    private dataTemp: any;
    private dataLum: any;
    private dataSoilHum: any;
    @ViewChildren("chart") charts: UIChart[];

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
        this.farmService.getEnvInfoToday(this.farmComponentId).then(
            (data: EnvironmentInfoModel[]) => {
                this.envData = data;
                this.processData(this.envData);
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

    processData(envData: EnvironmentInfoModel[]) {
        this.getLabelsX(envData);
        this.processDataForTempChart(envData);
        this.processDataForLumChart(envData);
        this.processDataForSoilChart(envData);
    }

    getLabelsX(envData: EnvironmentInfoModel[]) {
        this.labels = envData.map((envInfo: EnvironmentInfoModel) => {
            return envInfo.Timestamp.toLocaleString().slice(11);
        });
    }

    processDataForTempChart(envData: EnvironmentInfoModel[]) {
        let data: number[] = envData.map((envInfo: EnvironmentInfoModel) => {
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

    processDataForLumChart(envData: EnvironmentInfoModel[]) {
        let data: number[] = envData.map((envInfo: EnvironmentInfoModel) => {
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

    processDataForSoilChart(envData: EnvironmentInfoModel[]) {
        let data: number[] = envData.map((envInfo: EnvironmentInfoModel) => {
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

    OnSetDataForCharts() {
        //this.charts[0].data = this.dataTemp; //reset data for Temperature Chart
        //this.charts[1].data = this.dataLum; //reset data for Luminosity Chart
        //this.charts[2].data = this.dataSoilHum; //reset data for Soil Humidity Chart
    }

    OnChangeDate(event: Date) {
        let day = event.getUTCDate() + 1;
        let month = event.getUTCMonth() + 1;
        let year = event.getUTCFullYear();
        this.farmService.getEnvInfoWithDate(this.farmComponentId, day, month, year).then(
            (data: EnvironmentInfoModel[]) => {
                this.processData(data);
                //this.OnSetDataForCharts();
                this.charts.forEach(chart => setTimeout(()=> chart.reinit(),100));
            },
            (error: any) => {

            }
        );
    }
}