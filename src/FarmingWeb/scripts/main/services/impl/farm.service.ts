import { Injectable,EventEmitter } from "@angular/core";
import { Http, Headers, RequestOptions, Response } from "@angular/http";

import { FarmModel } from './../../models/farm.model';
import { FarmComponentModel } from './../../models/farm-component.model';
import { FarmComponentWithPlantModel } from './../../models/farmcmp-plant.model';
import { EnvOverallInfoModel } from './../../models/env-overall-info.model';
import { EnvironmentInfoModel } from './../../models/environment-information.model';

import { IFarmService } from './../interface/farm-service.interface';

import { AppSetting } from './../../../app/app.setting';
const headers: Headers = new Headers({ "Content-Type": "application/json" });
const options: RequestOptions = new RequestOptions({ headers: headers });

@Injectable()
export class FarmService implements IFarmService {

    constructor(
        protected http: Http
    ) { }

    getFarms(): Promise<FarmModel[]> {
        return new Promise<any>((resolve: any, reject: any) => {
            this.http.get(AppSetting.API_ENDPOINT + '/farms/getUserFarms').subscribe(
                (data: Response) => {
                    let listFarms = <FarmModel[]>data.json();
                    resolve(listFarms);
                },
                (error: any) => {
                    reject(error);
                }
            );
        });
    }

    getFarmComponent(farmId: number): Promise<FarmComponentModel[]> {
        return new Promise<any>((resolve: any, reject: any) => {
            let filter = '?farmID=' + farmId;
            return this.http.get(AppSetting.API_ENDPOINT + '/farms/getFarmComponents' + filter).subscribe(
                (data: Response) => {
                    let listFarmComponents = <FarmComponentModel[]>data.json();
                    resolve(listFarmComponents);
                },
                (error: any) => {
                    reject(error);
                }
            );
        });
    }

    createFarm(farm: FarmModel): Promise<any> {
        return new Promise<any>((resolve: any, reject: any) => {
            return this.http.post(AppSetting.API_ENDPOINT + '/farms/newFarm/', farm, options).subscribe(
                (data: Response) => {
                    resolve();
                },
                (error: any) => {
                    reject(error);
                }
            );
        });
    }

    createFarmComponent(farmComponent: FarmComponentWithPlantModel): Promise<any> {
        return new Promise<any>((resolve: any, reject: any) => {
            return this.http.post(AppSetting.API_ENDPOINT + '/farms/newFarmComponent/', farmComponent, options).subscribe(
                (data: Response) => {
                    resolve();
                },
                (error: any) => {
                    reject(error);
                }
            );
        });
    }

    getEnvOverallMonth(farmComponentId: number): Promise<EnvOverallInfoModel> {
        return new Promise<any>((resolve: any, reject: any) => {
            return this.http.get(AppSetting.API_ENDPOINT + '/farms/report/overallmonth/' + farmComponentId).subscribe(
                (data: Response) => {
                    let envOverallInfo = <EnvOverallInfoModel>data.json();
                    resolve(envOverallInfo);
                },
                (error: any) => {
                    reject(error);
                }
            );
        });
    }

    getEnvInfoToday(farmComponentId: number): Promise<EnvironmentInfoModel[]> {
        return new Promise<any>((resolve: any, reject: any) => {
            return this.http.get(AppSetting.API_ENDPOINT + '/farms/report/today/' + farmComponentId).subscribe(
                (data: Response) => {
                    let listEnvInfoToday = <EnvironmentInfoModel[]>data.json();
                    resolve(listEnvInfoToday);
                },
                (error: any) => {
                    reject(error);
                }
            );
        });
    }
}