import { Injectable } from "@angular/core";
import { Http, Headers, RequestOptions, Response } from "@angular/http";

import { FarmModel } from './../../models/farm.model';
import { FarmComponentModel } from './../../models/farm-component.model';

import { IFarmService } from './../interface/farm-service.interface';

import { AppSetting } from './../../../app/app.setting';

@Injectable()
export class FarmService implements IFarmService {
    constructor(
        protected http: Http
    ) { }

    getFarms(userID: number): Promise<FarmModel[]> {
        return new Promise<any>((resolve: any, reject: any) => {
            let filter = '?userID=' + userID;
            this.http.get(AppSetting.API_ENDPOINT + '/farms/getUserFarms' + filter).subscribe(
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
}