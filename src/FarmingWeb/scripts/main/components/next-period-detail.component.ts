import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Http } from '@angular/http';

import { PeriodDetail } from './../models/period-detail.model';
import { ChangePeriodSignal, SignalPeriod } from './../models/change-period-signal.model';

import { IPlantService } from './../services/interface/plant-service.interface';
import { NotificationService } from './../services/impl/notification.service';
import { ConfirmationService } from 'primeng/primeng';
@Component({
    selector: 'next-period-detail',
    templateUrl: './templates/main/components/next-period-detail.component.html',
    styleUrls: ['css/w3-farming.css'],
    providers: [ConfirmationService]
})
export class NextPeriodDetailComponent implements OnInit {
    public periodDetail: PeriodDetail = null;
    public signalPeriod: SignalPeriod = null;
    public canChangePeriod: boolean = false;
    private sub: any;
    private farmComponentId : number = 0;

    private imgTemp: string = "../../../images/temperature.png";
    private imgLum: string = "../../../images/lamp.png";
    private imgSoil: string = "../../../images/liquid.png";

    private dataTemp: string = "";
    private dataLum: string = "";
    private dataSoilHum: string = "";

    constructor(private http: Http,
        private activatedRoute: ActivatedRoute,
        private router: Router,
        private plantService: IPlantService,
        private notificationService: NotificationService,
        private confirmService: ConfirmationService
    ) {
        this.sub = this.activatedRoute.params.subscribe(params => {
            this.farmComponentId = +params['id'];
        });
    }

    ngOnInit() {
        this.plantService.askChangePeriod(this.farmComponentId).then(
            (data: ChangePeriodSignal) => {
                this.signalPeriod = data.Signal;
                if (this.signalPeriod == SignalPeriod.IsAvailable) {
                    this.canChangePeriod = true;
                } else {
                    this.canChangePeriod = false;
                }
            },
            (error: any) => {
                this.canChangePeriod = false;
            }
        );
        this.plantService.getNextPeriod(this.farmComponentId).then(
            (data: PeriodDetail) => {
                this.periodDetail = data;
                this.dataTemp = this.periodDetail.Temp_Min + " - " + this.periodDetail.Temp_Max;
                this.dataLum = this.periodDetail.Luminosity_Min + " - " + this.periodDetail.Luminosity_Max;
                this.dataSoilHum = this.periodDetail.Soil_Hum_Min + " - " + this.periodDetail.Soil_Hum_Max;
            },
            (error: any) => {
                this.router.navigate(['pagenotfound']);
            }
        );
    }

    onChangePeriod() {
        this.plantService.changePeriod(this.farmComponentId).then(
            (data: any) => {
                this.notificationService.emitter.emit({ severity: 'success', summary: 'Change Period', detail: 'Change Period Successfully' });
                this.router.navigate(['/farmiot/main']);
            },
            (error: any) => {
                this.notificationService.emitter.emit({ severity: 'error', summary: 'Change Period', detail: 'Change Period Failed' });
            }
        );
    }

    onConfirm() {
        this.confirmService.confirm({
            message: 'Bạn có chắc là muốn đổi chu kỳ cho cây?',
            header: 'Xác nhận',
            icon: 'fa fa-question-circle',
            accept: () => {
                this.onChangePeriod();
            },
            reject: () => {
                this.notificationService.emitter.emit({ severity: 'info', summary: 'Change Period', detail: 'Rejected Change Period' });
            }
        });
    }
}