import { Component, OnInit, AfterViewInit, OnDestroy, ViewChildren, QueryList } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UIChart } from 'primeng/primeng'
import { IntervalObservable } from "rxjs/observable/IntervalObservable";

import { EnvironmentInfoModel } from './../models/environment-information.model';

import { IFarmService } from './../services/interface/farm-service.interface';

//This component is stand for charts show environment data
@Component({
    selector: 'env-chart',
    templateUrl: './templates/main/components/env-chart.component.html'
})
export class EnvChartComponent implements OnInit, OnDestroy, AfterViewInit {
    private sub: any;
    private farmComponentId: number = 0;
    private envData: EnvironmentInfoModel[] = null;
    private choosenDate: Date = new Date();
    private maxDate: Date = new Date();
    private dateFormat: string = "dd/mm/yy"
    private labels: string[] = [];
    private dataTemp: any;
    private dataLum: any;
    private dataSoilHum: any;
    @ViewChildren("chart") charts: UIChart[];

    private intervalDataTodaySub: any;
    private intervalDataDateSub: any;

    //constructor
    constructor(
        private activatedRoute: ActivatedRoute,
        private router: Router,
        private farmService: IFarmService
    ) {
        // Get Id of FarmComponent
        this.sub = this.activatedRoute.params.subscribe(params => {
            this.farmComponentId = +params['id'];
        });
    }

    //Function will be called after constructor
    ngOnInit() {
        //Get Environemnt Data of Today with interval 5s
        this.intervalDataTodaySub =
            IntervalObservable.create(5000)
            .subscribe(() => {
                    this.farmService.getEnvInfoToday(this.farmComponentId).then(
                        (data: EnvironmentInfoModel[]) => {
                            this.envData = data;
                            this.processData(this.envData);
                            this.charts.forEach(chart => setTimeout(() => chart.reinit(), 100));
                        },
                        (error: any) => {
                            if (this.intervalDataTodaySub)
                                this.intervalDataTodaySub.unsubscribe();
                        }
                    );
                });
    }

    //Function will be called after user change page
    ngOnDestroy() {
        if (this.sub)
            this.sub.unsubscribe();
        if (this.intervalDataTodaySub)
            this.intervalDataTodaySub.unsubscribe();
        if (this.intervalDataDateSub)
            this.intervalDataDateSub.unsubscribe();
    }

    ////Function will be called after view of component rendered
    ngAfterViewInit() {
    }

    //Process Environment data for showing in charts
    processData(envData: EnvironmentInfoModel[]) {
        this.getLabelsX(envData);
        this.processDataForTempChart(envData);
        this.processDataForLumChart(envData);
        this.processDataForSoilChart(envData);
    }

    //Get Lables for charts (X)
    getLabelsX(envData: EnvironmentInfoModel[]) {
        this.labels = envData.map((envInfo: EnvironmentInfoModel) => {
            return envInfo.Timestamp.toLocaleString().slice(11);
        });
    }

    //Process Temperature Data for "Temperature" Chart
    processDataForTempChart(envData: EnvironmentInfoModel[]) {
        let data: number[] = envData.map((envInfo: EnvironmentInfoModel) => {
            return envInfo.Temperature;
        });
        this.dataTemp = {
            labels: this.labels,
            datasets: [
                {
                    label: 'Nhiệt độ',
                    data: data,
                    fill: false,
                    borderColor: '#f42a04'
                }
            ]
        }
    }

    //Process Luminosity data for "Luminosity" Chart
    processDataForLumChart(envData: EnvironmentInfoModel[]) {
        let data: number[] = envData.map((envInfo: EnvironmentInfoModel) => {
            return envInfo.Luminosity;
        });
        this.dataLum = {
            labels: this.labels,
            datasets: [
                {
                    label: 'Ánh sáng',
                    data: data,
                    fill: false,
                    borderColor: '#565656'
                }
            ]
        }
    }

    ////Process Soil Humidity data for "Soil Humidity" Chart
    processDataForSoilChart(envData: EnvironmentInfoModel[]) {
        let data: number[] = envData.map((envInfo: EnvironmentInfoModel) => {
            return envInfo.Soil_Humidity;
        });
        this.dataSoilHum = {
            labels: this.labels,
            datasets: [
                {
                    label: 'Độ ẩm đất',
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

    //Function called when user change data to watch environment data of that date
    OnChangeDate(event: Date) {
        if (this.intervalDataTodaySub)
            this.intervalDataTodaySub.unsubscribe();
        if (this.intervalDataDateSub)
            this.intervalDataDateSub.unsubscribe();

        let day = event.getUTCDate() + 1;
        let month = event.getUTCMonth() + 1;
        let year = event.getUTCFullYear();

        //Get Environemnt Data of Date we choose with interval 5s
        this.intervalDataDateSub =
            IntervalObservable.create(5000)
                .subscribe(() => {
                    this.farmService.getEnvInfoWithDate(this.farmComponentId, day, month, year).then(
                        (data: EnvironmentInfoModel[]) => {
                            this.processData(data);
                            //this.OnSetDataForCharts();
                            this.charts.forEach(chart => setTimeout(() => chart.reinit(), 100));
                        },
                        (error: any) => {

                        }
                    );
                });
    }
}