import { Component, OnInit, Input, ViewChild, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';
import { Http } from '@angular/http';
import { FormGroup, Validators, FormBuilder } from "@angular/forms";
import { GMap } from 'primeng/primeng';

import { FarmModel } from './../models/farm.model';

import { IAuthenticateService } from './../services/interface/authenticate.-service.interface';
import { IFarmService } from './../services/interface/farm-service.interface';
declare var google: any;
@Component({
    selector: 'farm-create',
    templateUrl: './templates/main/components/farm-create.component.html'
})
export class CreateFarmComponent implements OnInit, AfterViewInit{
    public options: any = {
        center: { lat: 10.7687085, lng: 106.4141646 },
        zoom: 14,
        minZoom: 10,
        maxZoom: 21
    };
    @ViewChild('gmap') gmap: GMap;
    public farmForm: FormGroup;
    public farm: FarmModel = new FarmModel();
    public map: any;
    private numPoints: number = 0;
    private pointArr: any = [];
    private markerArr: any = [];
    private polygon: any = null;
    constructor(
        private fb: FormBuilder,
        private router: Router,
        private http: Http,
        private authenticateService: IAuthenticateService,
        private farmService: IFarmService
    ) {
        this.farmForm = this.fb.group({
            Name: [""],
            Address: ["", Validators.required]
        });
    }

    ngOnInit() {

    }

    ngAfterViewInit() {
        this.map = this.gmap.getMap();
    }

    onMapClick(event: any) {
        let lat: number = event.latLng.lat();
        let lng: number = event.latLng.lng();
        if (this.numPoints < 2) {
            this.drawMarkers(lat, lng);
        } else {
            this.drawPolygon(lat, lng);
            this.bindingBoundary();
            this.farm.Position_Lat = lat;
            this.farm.Position_Lng = lng;
        }
    }

    drawMarkers(lat: number, lng: number) {
        let marker: any = new google.maps.Marker({
            position: {
                lat: lat,
                lng: lng
            }
        });
        this.markerArr.push(marker);
        this.pointArr.push({
            lat: lat,
            lng: lng
        });
        marker.setMap(this.map);
        this.numPoints++;
    }

    drawPolygon(lat: number, lng: number) {
        if (this.markerArr.length > 0) {
            this.markerArr.forEach(marker => marker.setMap(null));
        }
        this.pointArr.push({
            lat: lat,
            lng: lng
        });
        if (!this.polygon) {
            this.polygon = new google.maps.Polygon({
                paths: this.pointArr,
                strokeColor: '#FF0000',
                strokeOpacity: 0.8,
                strokeWeight: 3,
                fillColor: '#FF0000',
                fillOpacity: 0.35
            });
            this.polygon.setMap(this.map);
        } else {
            this.polygon.setPaths(this.pointArr);
        }
        this.numPoints++;
    }

    bindingBoundary() {
        let boundaryStr: string = '';
        this.pointArr.forEach(point => {
            boundaryStr += '(' + point.lat + ';' + point.lng + '),';
        });
        boundaryStr = '[' + boundaryStr.substring(0, boundaryStr.length - 1) + ']';
        this.farm.Boundary = boundaryStr;
    }

    onCreatFarm() {
        this.farmService.createFarm(this.farm).then(
            (data: any) => {
                this.router.navigate(['farmiot/main']);
            },
            (error: any) => {
                console.log('error');
            }
        );
    }
}