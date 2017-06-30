import { Injectable } from "@angular/core";
import { PlantDetailModel } from './../../models/plant-detail.model';
import { PlantModel } from './../../models/plant.model';
@Injectable()
export abstract class IPlantService {
    abstract getPlantDetail(farmComponentId: number): Promise<PlantDetailModel[]>;
    abstract getAllPlant(): Promise<PlantModel[]>;
}
