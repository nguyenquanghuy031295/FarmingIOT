import { Http, Headers, RequestOptions, Response } from "@angular/http";
import { Router, ActivatedRouteSnapshot } from "@angular/router";
import { Injectable } from "@angular/core";

import { LoginData } from "./../../models/login.model";
import { RegisterModel } from "./../../models/register.model";
import { AccountInfoModel } from './../../models/account-info.model';
import { UserModel } from './../../models/user.model';

import { IAuthenticateService } from './../interface/authenticate.-service.interface';

import { AppSetting } from './../../../app/app.setting';
const headers: Headers = new Headers({ "Content-Type": "application/json" });
const options: RequestOptions = new RequestOptions({ headers: headers });

@Injectable()
export class AuthenticateService implements IAuthenticateService {
    public user: UserModel = null;
    constructor(protected http: Http
    ) { }

    login(user: LoginData): Promise<any> {
        return new Promise<any>((resolve: any, reject: any) => {
            this.http.post(AppSetting.API_ENDPOINT + '/authentication/signin', user, options).subscribe(
                (res: Response) => {
                    console.log(res);
                    resolve();
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

    getAccontInfo(): Promise<AccountInfoModel> {
        return new Promise<AccountInfoModel>((resolve: any, reject: any) => {
            this.http.get(AppSetting.API_ENDPOINT + '/authentication/accountInfo').subscribe(
                (data: Response) => {
                    let accountInfo = <AccountInfoModel>data.json();
                    resolve(accountInfo);
                },
                (error: any) => {
                    reject(error);
                }
            );
        });
    }

    editAccountInfo(userInfo: AccountInfoModel): Promise<any> {
        return new Promise<void>((resolve: any, reject: any) => {
            let userInfoCommand = {
                Name: userInfo.Name,
                DOB: userInfo.DOB,
                Address: userInfo.Address
            };
            this.http.post(AppSetting.API_ENDPOINT + '/authentication/accountInfo', userInfoCommand, options).subscribe(
                (res: Response) => {
                    resolve();
                },
                (error: any) => {
                    reject(error);
                });
        });
    }
}