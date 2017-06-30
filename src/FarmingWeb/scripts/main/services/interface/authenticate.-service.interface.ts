import { Injectable } from "@angular/core";
import { LoginData } from './../../models/login.model';
import { RegisterModel } from './../../models/register.model';
import { AccountInfoModel } from './../../models/account-info.model';

@Injectable()
export abstract class IAuthenticateService {
    abstract userID: number;
    abstract login(user: LoginData): Promise<any>;
    abstract register(user: RegisterModel): Promise<void>;
    abstract getAccontInfo(): Promise<AccountInfoModel>;
    abstract editAccountInfo(userInfo: AccountInfoModel): Promise<any>;
}
