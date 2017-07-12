import { Injectable } from "@angular/core";
import { LoginData } from './../../models/login.model';
import { RegisterModel } from './../../models/register.model';
import { AccountInfoModel } from './../../models/account-info.model';
import { UserModel } from './../../models/user.model';

@Injectable()
export abstract class IAuthenticateService {
    abstract user: UserModel;
    abstract login(user: LoginData): Promise<any>;
    abstract register(user: RegisterModel): Promise<void>;
    abstract getAccontInfo(): Promise<AccountInfoModel>;
    abstract editAccountInfo(userInfo: AccountInfoModel): Promise<any>;
}
