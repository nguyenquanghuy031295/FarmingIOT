import { Injectable } from "@angular/core";
import { CanActivate, ActivatedRouteSnapshot } from '@angular/router';
import { LoginData } from './../../models/login.model';
import { RegisterModel } from './../../models/register.model';
import { AccountInfoModel } from './../../models/account-info.model';
import { UserModel } from './../../models/user.model';

@Injectable()
export abstract class IAuthenticateService implements CanActivate {
    abstract user: UserModel;
    abstract login(user: LoginData): Promise<any>;
    abstract register(user: RegisterModel): Promise<void>;
    abstract getAccontInfo(): Promise<AccountInfoModel>;
    abstract editAccountInfo(userInfo: AccountInfoModel): Promise<any>;
    abstract canActivate(route: ActivatedRouteSnapshot): Promise<boolean>;
    abstract logout(): Promise<void>;
}
