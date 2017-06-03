import { Injectable } from "@angular/core";
import { LoginData } from './../../models/login.model';
import { RegisterModel } from './../../models/register.model';
@Injectable()
export abstract class IAuthenticateService {
    abstract userID: number;
    abstract login(user: LoginData): Promise<any>;
    abstract register(user: RegisterModel): Promise<void>;
}
