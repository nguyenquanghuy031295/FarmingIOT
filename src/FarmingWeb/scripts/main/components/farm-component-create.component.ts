import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router'
import { SelectItem } from 'primeng/primeng';

import { PlantModel } from './../models/plant.model';
import { FarmComponentWithPlantModel } from './../models/farmcmp-plant.model'
import { IFarmService } from './../services/interface/farm-service.interface';
import { IPlantService } from './../services/interface/plant-service.interface';
@Component({
    selector: 'farm-cmp-create',
    templateUrl: './templates/main/components/farm-component-create.component.html'
})
export class CreateFarmCmpComponent implements OnDestroy, OnInit {
    private display: boolean = false;
    private farmId: number;
    private plants: SelectItem[] = [];
    private selectedPlant: PlantModel;
    private sub: any;
    private newFarmCmp: FarmComponentWithPlantModel = new FarmComponentWithPlantModel();
    private isDisable: boolean = false;
    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private farmService: IFarmService,
        private plantService: IPlantService
    ) {
        this.plants.push({ label: 'Select Plant', value: null });

        this.sub = this.activatedRoute.params.subscribe(params => {
            this.farmId = +params['id'];
        });

        this.plantService.getAllPlant().then(
            (data: PlantModel[]) => {
                data.forEach((plant: PlantModel) => {
                    this.plants.push({
                        label: plant.Name,
                        value: plant
                    });
                });
            },
            (error: any) => {

            }
        );
    }

    ngOnInit() {

    }

    ngOnDestroy() {
        if (this.sub)
            this.sub.unsubscribe();
    }

    onChangePlant(event: any) {
        
    }

    onCreate() {
        this.newFarmCmp.PlantKBId = this.selectedPlant.PlantKBId;
        this.newFarmCmp.FarmId = this.farmId;
        this.newFarmCmp.StartPlantDate = new Date();
        //
        this.isDisable = true;
        this.farmService.createFarmComponent(this.newFarmCmp).then(
            (data: any) => {
                this.router.navigate(['farmiot/main']);
            },
            (error: any) => {
                this.isDisable = false;
            }
        )
    }
}