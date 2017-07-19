import { Injectable, EventEmitter } from "@angular/core";

import { FarmModel } from './../../models/farm.model';
import { FarmComponentModel } from './../../models/farm-component.model';
import { FarmComponentWithPlantModel } from './../../models/farmcmp-plant.model';
import { EnvOverallInfoModel } from './../../models/env-overall-info.model';
import { EnvironmentInfoModel } from './../../models/environment-information.model';

@Injectable()
export abstract class IFarmService {
    abstract getFarms(): Promise<FarmModel[]>;
    abstract getFarmComponent(farmId: number): Promise<FarmComponentModel[]>;
    abstract getEnvOverallMonth(farmComponentId: number): Promise<EnvOverallInfoModel>;
    abstract getEnvInfoToday(farmComponentId: number): Promise<EnvironmentInfoModel[]>;
    abstract getEnvLatest(farmComponentId: number): Promise<EnvironmentInfoModel>;
    abstract createFarm(farm: FarmModel): Promise<any>;
    abstract createFarmComponent(farmComponent: FarmComponentWithPlantModel): Promise<any>;
    
}
