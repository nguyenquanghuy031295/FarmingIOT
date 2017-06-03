import { Injectable } from "@angular/core";
import { PlantDetailModel } from './../../models/plant-detail.model';
@Injectable()
export abstract class IPlantService {
    abstract getPlantDetail(farmComponentId: number): Promise<PlantDetailModel[]>;
}
