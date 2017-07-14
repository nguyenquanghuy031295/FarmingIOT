import { Component, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';
import { Http, Headers, RequestOptions, Response } from "@angular/http";
import { FormGroup, Validators, FormBuilder } from "@angular/forms";

import { AppSetting } from './../../app/app.setting';
import { LoginData } from './../models/login.model';
import { IAuthenticateService } from './../services/interface/authenticate.-service.interface';
import { NotificationService } from './../services/impl/notification.service';

const headers: Headers = new Headers({ "Content-Type": "application/json" });
const options: RequestOptions = new RequestOptions({ headers: headers });
declare var jQuery: any;
@Component({
    selector: 'login',
    templateUrl: './templates/main/components/signin.component.html',
    styleUrls: ['./css/main/components/signin.component.css']
})
export class SignInComponent implements AfterViewInit{
    public loginForm: FormGroup;
    public user: LoginData = new LoginData();
    constructor(
        private fb: FormBuilder,
        private router: Router,
        private http: Http,
        private authenticateService: IAuthenticateService,
        private notificationService: NotificationService
    ) {
        this.loginForm = this.fb.group({
            Email: ["", [Validators.required, Validators.maxLength(20)]],
            Password: ["", [Validators.required, Validators.minLength(8)]],
        });
    }

    ngAfterViewInit() {
        //jQuery('#loader').removeClass('sk-circle');
        //jQuery('#body').removeClass('body-colour');
    }

    OnLogin() {
        this.authenticateService.login(this.user).then(
            (data: any) => {
                this.notificationService.emitter.emit({ severity: 'success', summary: 'Login', detail: 'Login Successfully' });
                this.router.navigate(['/farmiot/main']);
            },
            (error: any) => {
                this.notificationService.emitter.emit({ severity: 'error', summary: 'Login', detail: 'Login Failed' });
            }
        );
    }

    ChangeToSignUp() {
        this.router.navigate(["/signup"]);
    }
}