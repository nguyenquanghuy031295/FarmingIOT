import { Injectable } from "@angular/core";
import { Http, Headers, RequestOptions, Response } from "@angular/http";

import { EnvironmentInfoModel } from './../../models/environment-information.model';

import { AppSetting } from './../../../app/app.setting';

import { IDeviceService } from './../interface/device-service.interface';
@Injectable()
export class DeviceService implements IDeviceService {
    constructor(
        protected http: Http
    ) { }

    GetSensorData(farmComponentId: number): Promise<EnvironmentInfoModel[]> {
        return new Promise<any>((resolve: any, reject: any) => {
            let filter: string = '?farmComponentID=' + farmComponentId;
            this.http.get(AppSetting.API_ENDPOINT + '/devices/getDataSensor' + filter).subscribe(
                (data: Response) => {
                    let listEnvironmentInfo = <EnvironmentInfoModel>data.json();
                    resolve(listEnvironmentInfo);
                },
                (error: any) => {
                    reject(error);
                }
            );
        });
    }
}