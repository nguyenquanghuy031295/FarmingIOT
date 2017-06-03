import { Injectable } from "@angular/core";

import { FarmModel } from './../../models/farm.model';
import { FarmComponentModel } from './../../models/farm-component.model';
@Injectable()
export abstract class IFarmService {
    abstract getFarms(userID: number): Promise<FarmModel[]>;
    abstract getFarmComponent(farmId: number): Promise<FarmComponentModel[]>;
}
