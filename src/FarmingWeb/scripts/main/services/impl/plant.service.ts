import { Injectable } from "@angular/core";
import { Http, Headers, RequestOptions, Response } from "@angular/http";

import { PlantDetailModel } from './../../models/plant-detail.model';

import { IPlantService } from './../interface/plant-service.interface';

import { AppSetting } from './../../../app/app.setting';

@Injectable()
export class PlantService implements IPlantService {
    constructor(
        protected http: Http
    ) { }

    getPlantDetail(farmComponentId: number): Promise<PlantDetailModel[]> {
        return new Promise<any>((resolve: any, reject: any) => {
            let filter = '?farmComponentId=' + farmComponentId;
            this.http.get(AppSetting.API_ENDPOINT + '/plants/detail' + filter).subscribe(
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
}