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
    constructor(
        protected http: Http,
        private router: Router
    ) { }

    login(user: LoginData): Promise<any> {
        return new Promise<any>((resolve: any, reject: any) => {
            this.http.post(AppSetting.API_ENDPOINT + '/authentication/signin', user, options).subscribe(
                (res: Response) => {
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

    canActivate(route: ActivatedRouteSnapshot): Promise<boolean> {
        let url: string = route.url.join('/');
        if (url.startsWith("signin")) {
            return this.getAccontInfo().then(
                () => {
                    this.router.navigate(['farmiot/main']);
                    return false;
                }, () => {
                    return true;
                }
            );
        } else if (url.startsWith("farmiot")) {
            if (this.user) {
                return Promise.resolve(true);
            }
            return this.getAccontInfo().then(
                () => {
                    return true;
                }, () => {
                    return false;
                }
            );
        }
        else {
            Promise.resolve(true);
        }
    }

    logout(): Promise<void> {
        return new Promise<void>((resolve: any, reject: any) => {
            this.http.post(AppSetting.API_ENDPOINT + '/authentication/signout', null , options).subscribe(
                (res: Response) => {
                    resolve();
                },
                (error: any) => {
                    reject(error);
                });
        });
    }

    changePassword(currentPassword: string, newPassword: string): Promise<void> {
        let changePasswordCommand: any = {
            OldPassword: currentPassword,
            NewPassword: newPassword
        };
        return new Promise<void>((resolve: any, reject: any) => {
            this.http.post(AppSetting.API_ENDPOINT + '/authentication/changePassword', changePasswordCommand, options).subscribe(
                (res: Response) => {
                    resolve();
                },
                (error: any) => {
                    reject(error);
                }
            );
        });
    }
}