import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common'
import { HttpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
    GMapModule,
    DropdownModule,
    ButtonModule,
    PanelModule,
    DataTableModule,
    ChartModule,
    FieldsetModule,
    SharedModule,
    CalendarModule,
    DialogModule
} from 'primeng/primeng';

import { MainRoutesModule } from './../routes/main-routes.module';
import { ShareModule } from './../../app/share.module';

import { HelloComponent } from './../components/hello.component';
import { SignInComponent } from './../components/signin.component';
import { SignupComponent } from './../components/signup.component';
import { GoogleMapComponent } from './../components/google-map.component';
import { MainPageComponent } from './../components/main-page.component';
import { ControlSensorComponent } from './../components/control-sensor.component';
import { AppMenuComponent } from './../components/app-menu.component';
import { MainComponent } from './../components/main.component';
import { AccountInfoComponent } from './../components/account-info.component';
import { CreateFarmComponent } from './../components/farm-create.component';
import { CreateFarmCmpComponent } from './../components/farm-component-create.component';
import { ReportComponent } from './../components/report.component';
import { EnvCardComponent } from './../components/env-card.component';
import { EnvironmentLatestComponent } from './../components/environment-latest.component';
import { EnvChartComponent } from './../components/env-chart.component';

import { DateFormatPipe } from './../pipes/date-format.pipe';

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
        RouterModule,
        HttpModule,
        ShareModule,
        FormsModule,
        ReactiveFormsModule,
        BrowserModule,
        GMapModule,
        DropdownModule,
        ButtonModule,
        PanelModule,
        DataTableModule,
        ChartModule,
        FieldsetModule,
        CalendarModule,
        DialogModule,
        SharedModule,
        CommonModule
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    exports: [],
    declarations: [
        HelloComponent,
        SignInComponent,
        SignupComponent,
        GoogleMapComponent,
        MainPageComponent,
        MainComponent,
        EnvCardComponent,
        DateFormatPipe,
        EnvChartComponent,
        EnvironmentLatestComponent, //fix
        ReportComponent, //fix
        AppMenuComponent, //fix
        AccountInfoComponent, //fix
        CreateFarmComponent, //fix
        CreateFarmCmpComponent, // fix
        ControlSensorComponent //test
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

