import { Http, Headers, RequestOptions, Response } from "@angular/http";
import { Router, ActivatedRouteSnapshot } from "@angular/router";
import { Injectable } from "@angular/core";

import { LoginData } from "./../../models/login.model";
import { RegisterModel } from "./../../models/register.model";

import { IAuthenticateService } from './../interface/authenticate.-service.interface';

import { AppSetting } from './../../../app/app.setting';
const headers: Headers = new Headers({ "Content-Type": "application/json" });
const options: RequestOptions = new RequestOptions({ headers: headers });

@Injectable()
export class AuthenticateService implements IAuthenticateService {
    public userID: number = 0;
    constructor(protected http: Http
    ) { }

    login(user: LoginData): Promise<any> {
        return new Promise<any>((resolve: any, reject: any) => {
            this.http.post(AppSetting.API_ENDPOINT + '/authentication/signin', user, options).subscribe(
                (res: Response) => {
                    resolve(res.json());
                },
                (error: any) => {
                    reject(error);
                });
        });
    }

    register(user: RegisterModel): Promise<void> {
        return new Promise<void>((resolve: any, reject: any) => {
            this.http.post(AppSetting.API_ENDPOINT + '/authentication/signup', user, options).subscribe(
                (res: Response) => {
                    resolve();
                },
                (error: any) => {
                    reject(error);
                });
        });
    }
}