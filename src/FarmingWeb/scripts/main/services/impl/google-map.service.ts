import { Injectable, EventEmitter } from "@angular/core";
import { Http, Headers, RequestOptions, Response } from "@angular/http";
import { FarmModel } from './../../models/farm.model';
//
@Injectable()
export class GoogleMapService {
    public farmLocationEmitter: EventEmitter<FarmModel> = new EventEmitter<FarmModel>();
}