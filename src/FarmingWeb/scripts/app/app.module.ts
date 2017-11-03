﻿import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { LocationStrategy, HashLocationStrategy } from '@angular/common';

import { GrowlModule } from 'primeng/primeng';
import { AppRoutesModule } from './app-routes.module';
import { MainModule } from './../main/modules/main.module';

import { AppComponent } from './app.component';
import { NullComponent } from './null.component';

import { NotificationService } from './../main/services/impl/notification.service';
//Root Module is App Module. This module is used for boostrap to browser
@NgModule({
    //imports some modules related
    imports: [
        BrowserModule,
        AppRoutesModule,
        MainModule,
        GrowlModule,
    ],
    exports: [],
    declarations: [
        AppComponent,
        NullComponent,
    ],
    bootstrap: [AppComponent],
    // providers is Dependency Injection for registering services
    providers: [
        NotificationService,
        { provide: LocationStrategy, useClass: HashLocationStrategy },
    ],
})
export class AppModule { }