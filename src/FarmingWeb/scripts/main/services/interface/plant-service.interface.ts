import { Injectable } from "@angular/core";

import { PlantDetailModel } from './../../models/plant-detail.model';
import { PlantModel } from './../../models/plant.model';
import { ChangePeriodSignal } from './../../models/change-period-signal.model';
import { PeriodDetail } from './../../models/period-detail.model';

@Injectable()
export abstract class IPlantService {
    abstract getPlantDetail(farmComponentId: number): Promise<PlantDetailModel[]>;
    abstract getAllPlant(): Promise<PlantModel[]>;
    abstract askChangePeriod(farmComponentId: number): Promise<ChangePeriodSignal>;
    abstract getNextPeriod(farmComponentId: number): Promise<PeriodDetail>;
    abstract changePeriod(farmComponentId: number): Promise<void>;
}
