import { Component, OnInit, Input, ViewChild, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';
import { GMap } from 'primeng/primeng';

import { FarmModel } from './../models/farm.model';

import { GoogleMapService } from './../services/impl/google-map.service';

declare var google: any;
@Component({
    selector: 'gmap',
    templateUrl: './templates/main/components/google-map.component.html'
})
export class GoogleMapComponent implements OnInit, AfterViewInit{
    public options: any = {
        center: { lat: 36.890257, lng: 30.707417 },
        zoom: 14,
        minZoom: 10,
        maxZoom: 21
    };
    @ViewChild('gmap') gmap: GMap;
    public map: any;
    constructor(
        private router: Router,
        private googleMapService: GoogleMapService
    ) {
    }

    ngOnInit() {
        let that = this;
        this.googleMapService.farmLocationEmitter.subscribe((farmLocation: FarmModel) => {
            that.map.setCenter({ lat: farmLocation.Position_Lat, lng: farmLocation.Position_Lng });
            that.drawFarmShape(farmLocation);
        });
    }

    drawFarmShape(farm: FarmModel) {
        if (farm.FarmId != 0) {
            //convert string to polygon points
            let regGex: RegExp = new RegExp('[0-9]+[.][0-9]+[;][0-9]+[.][0-9]+', 'g');
            var polygonPoints = [];
            var boundaryVal = regGex.exec(farm.Boundary);
            while (boundaryVal != null) {
                let point: string[] = boundaryVal[0].split(';');
                polygonPoints.push({ lat: +point[0], lng: +point[1] });
                boundaryVal = regGex.exec(farm.Boundary);
            }
            //draw polygon
            var bermudaTriangle = new google.maps.Polygon({
                paths: polygonPoints,
                strokeColor: '#FF0000',
                strokeOpacity: 0.8,
                strokeWeight: 3,
                fillColor: '#FF0000',
                fillOpacity: 0.35
            });
            bermudaTriangle.setMap(this.map);
        }
    }

    ngAfterViewInit() {
        this.map = this.gmap.getMap();
    }
}
