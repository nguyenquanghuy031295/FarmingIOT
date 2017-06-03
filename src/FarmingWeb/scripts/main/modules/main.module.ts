import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
    GMapModule,
    DropdownModule,
    ButtonModule,
    PanelModule,
    DataTableModule
} from 'primeng/primeng';

import { MainRoutesModule } from './../routes/main-routes.module';
import { ShareModule } from './../../app/share.module';

import { HelloComponent } from './../components/hello.component';
import { SignInComponent } from './../components/signin.component';
import { SignupComponent } from './../components/signup.component';
import { GoogleMapComponent } from './../components/google-map.component';
import { MainPageComponent } from './../components/main-page.component';
import { TestMQTTComponent } from './../components/testMQTT.component'; // test

import { IAuthenticateService } from './../services/interface/authenticate.-service.interface';
import { AuthenticateService } from './../services/impl/authenticate.serivce';

import { IFarmService } from './../services/interface/farm-service.interface';
import { FarmService } from './../services/impl/farm.service';

import { IPlantService } from './../services/interface/plant-service.interface';
import { PlantService } from './../services/impl/plant.service';

import { IDeviceService } from './../services/interface/device-service.interface';
import { DeviceService } from './../services/impl/device.service';

import { GoogleMapService } from './../services/impl/google-map.service';
@NgModule({
    imports: [
        MainRoutesModule,
        HttpModule,
        ShareModule,
        FormsModule,
        ReactiveFormsModule,
        BrowserModule,
        GMapModule,
        DropdownModule,
        ButtonModule,
        PanelModule,
        DataTableModule
    ],
    exports: [],
    declarations: [
        HelloComponent,
        SignInComponent,
        SignupComponent,
        GoogleMapComponent,
        MainPageComponent,
        TestMQTTComponent //test
    ],
    providers: [
        GoogleMapService,
        { provide: IAuthenticateService, useClass: AuthenticateService },
        { provide: IFarmService, useClass: FarmService },
        { provide: IPlantService, useClass: PlantService },
        { provide: IDeviceService, useClass: DeviceService },
    ]
})
export class MainModule { }

