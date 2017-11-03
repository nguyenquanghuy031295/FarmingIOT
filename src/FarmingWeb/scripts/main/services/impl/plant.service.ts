import { Injectable } from "@angular/core";
import { Http, Headers, RequestOptions, Response } from "@angular/http";

import { PlantDetailModel } from './../../models/plant-detail.model';
import { PlantModel } from './../../models/plant.model';
import { ChangePeriodSignal } from './../../models/change-period-signal.model';
import { PeriodDetail } from './../../models/period-detail.model';

import { IPlantService } from './../interface/plant-service.interface';

import { AppSetting } from './../../../app/app.setting';

const headers: Headers = new Headers({ "Content-Type": "application/json" });
const options: RequestOptions = new RequestOptions({ headers: headers });

//this service is used for some logics related to Plant
@Injectable()
export class PlantService implements IPlantService {
    constructor(
        protected http: Http
    ) { }

    //get Plant Detail of a FarmComponent
    getPlantDetail(farmComponentId: number): Promise<PlantDetailModel[]> {
        return new Promise<any>((resolve: any, reject: any) => {
            this.http.get(AppSetting.API_ENDPOINT + '/plants/detail/' + farmComponentId).subscribe(
                (data: Response) => {
                    let listPlantDetail = <PlantDetailModel[]>data.json();
                    resolve(listPlantDetail);
                },
                (error: any) => {
                    reject(error);
                }
            );
        });
    }

    //get All Plants in Database
    getAllPlant(): Promise<PlantModel[]> {
        return new Promise<any>((resolve: any, reject: any) => {
            this.http.get(AppSetting.API_ENDPOINT + '/plants/').subscribe(
                (data: Response) => {
                    let listPlantDetail = <PlantModel[]>data.json();
                    resolve(listPlantDetail);
                },
                (error: any) => {
                    reject(error);
                }
            );
        });
    }

    //ask Server plant in FarmComponent can change period now?
    askChangePeriod(farmComponentId: number): Promise<ChangePeriodSignal> {
        return new Promise<ChangePeriodSignal>((resolve: any, reject: any) => {
            this.http.get(AppSetting.API_ENDPOINT + '/plants/changePeriod/' + farmComponentId).subscribe(
                (data: Response) => {
                    let signal = <ChangePeriodSignal>data.json();
                    resolve(signal);
                },
                (error: any) => {
                    reject(error);
                }
            );
        });
    }

    //get next period Detail
    getNextPeriod(farmComponentId: number): Promise<PeriodDetail> {
        return new Promise<PeriodDetail>((resolve: any, reject: any) => {
            this.http.get(AppSetting.API_ENDPOINT + '/plants/nextPeriod/' + farmComponentId).subscribe(
                (data: Response) => {
                    let periodDetail = <PeriodDetail>data.json();
                    resolve(periodDetail);
                },
                (error: any) => {
                    reject(error);
                }
            );
        });
    }

    //Change period to next period of a Plant
    changePeriod(farmComponentId: number): Promise<void> {
        return new Promise<void>((resolve: any, reject: any) => {
            this.http.post(AppSetting.API_ENDPOINT + '/plants/changePeriod/' + farmComponentId, null, options).subscribe(
                (data: Response) => {
                    resolve();
                },
                (error: any) => {
                    reject(error);
                }
            );
        });
    }
}