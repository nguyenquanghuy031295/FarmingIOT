import { Injectable } from "@angular/core";
import { EnvironmentInfoModel } from './../../models/environment-information.model';
@Injectable()
export abstract class IDeviceService {
    abstract GetSensorData(farmComponentId: number): Promise<EnvironmentInfoModel[]>;
}
