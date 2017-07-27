import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SelectItem } from 'primeng/primeng';

import { FarmModel } from './../models/farm.model';
import { FarmComponentModel } from './../models/farm-component.model';
import { PlantDetailModel } from './../models/plant-detail.model';
import { EnvironmentInfoModel } from './../models/environment-information.model';
import { ChangePeriodSignal, SignalPeriod } from './../models/change-period-signal.model';

import { IFarmService } from './../services/interface/farm-service.interface';
import { IAuthenticateService } from './../services/interface/authenticate.-service.interface';
import { IPlantService } from './../services/interface/plant-service.interface';
import { IDeviceService } from './../services/interface/device-service.interface';
import { GoogleMapService } from './../services/impl/google-map.service';

@Component({
    selector: 'main-page',
    templateUrl: './templates/main/components/main-page.component.html'
})
export class MainPageComponent implements OnInit {

    public farms: SelectItem[] = [];
    public farmComponents: SelectItem[] = [];

    public selectedFarmComponent: number = 0;
    public selectedFarm: FarmModel = new FarmModel();
    //public watchDataSensor: boolean = false;
    public watchPlantDetail: boolean = false;
    public plantDetails: PlantDetailModel[] = [];
    public sensorData: EnvironmentInfoModel[] = [];
    public hasNextPeriod: boolean = false;
    public signalPeriod: SignalPeriod;
    public showDialogNextPeriod: boolean = false;

    constructor(
        private router: Router,
        private farmService: IFarmService,
        private authenticateService: IAuthenticateService,
        private plantService: IPlantService,
        private deviceService: IDeviceService,
        private googleMapService: GoogleMapService
    ) {
        this.farms.push({
            label: 'Select Farm',
            value: new FarmModel()
        });
    }

    onChangeFarm(event: any) {
        this.plantDetails = [];
        this.watchPlantDetail = false;
        //
        this.sensorData = [];
        //
        this.farmComponents = [];
        this.selectedFarmComponent = 0;
        
        this.googleMapService.farmLocationEmitter.emit(event.value);

        this.farmService.getFarmComponent(this.selectedFarm.FarmId).then(
            (data: FarmComponentModel[]) => {
                this.initialFarmComponentDropdown(data);
            },
            (error: any) => {

            }
        );
    }
    onChangeFarmComponent(event: any) {
        this.plantService.getPlantDetail(this.selectedFarmComponent).then(
            (data: PlantDetailModel[]) => {
                this.plantDetails = data;
            },
            (error: any) => {

            }
        );
        this.plantService.askChangePeriod(this.selectedFarmComponent).then(
            (data: ChangePeriodSignal) => {
                this.signalPeriod = data.Signal;
                if (data.Signal == SignalPeriod.IsAvailable || data.Signal == SignalPeriod.IsNotEnoughDay) {
                    this.hasNextPeriod = true;
                }
                else {
                    this.hasNextPeriod = false;
                }
            },
            (error: any) => {
                this.hasNextPeriod = false;
            }
        );
    }

    onWatchReport() {
        this.router.navigate(['farmiot/farm/component', this.selectedFarmComponent]);
    }

    onWatchPlantDetail() {
        this.watchPlantDetail = !this.watchPlantDetail;
    }

    ngOnInit() {
        this.farmService.getFarms().then(
            (data: FarmModel[]) => {
                data.forEach(farm => {
                    this.farms.push({
                        label: farm.Name,
                        value: farm
                    });
                });
            },
            (error: any) => {

            }
        );
    }

    createFarmDetail(farm: FarmModel): string {
        let farmDetail: string = `
            Address: ${farm.Address};
            Lat: ${farm.Position_Lat};
            Lng: ${farm.Position_Lng}
        `;
        return farmDetail;
    }

    initialFarmComponentDropdown(farmComponents: FarmComponentModel[]) {
        this.farmComponents = [];
        this.farmComponents.push({ label: 'Select Farm Component', value: 0 });
        farmComponents.forEach(farmComponent => {
            this.farmComponents.push({ label: farmComponent.Name, value: farmComponent.Farm_ComponentId });
        });
    }

    onCreateNewFarmCmp() {
        this.router.navigate(['farmiot/farm/', this.selectedFarm.FarmId, 'newfarmcmp']);
    }

    onWatchNextPeriod() {
        this.router.navigate(['farmiot/farm/component/nextperiod', this.selectedFarmComponent]);
    }
}
