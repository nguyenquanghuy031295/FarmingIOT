import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Http } from '@angular/http';
import { FormGroup, Validators, FormBuilder } from "@angular/forms";

import { AppSetting } from './../../app/app.setting';
import { RegisterModel } from './../models/register.model';

import { IAuthenticateService } from './../services/interface/authenticate.-service.interface';
import { NotificationService } from './../services/impl/notification.service';

//This Component is stand for SignUp Page
@Component({
    selector: 'signup',
    templateUrl: './templates/main/components/signup.component.html',
    styleUrls: ['./css/main/components/signup.component.css']
})
export class SignupComponent {
    public registerForm: FormGroup;
    public registerData: RegisterModel = new RegisterModel();

    //constructor
    constructor(
        private fb: FormBuilder,
        private router: Router,
        private http: Http,
        private authenticareService: IAuthenticateService,
        private notificationService: NotificationService
    ) {
        this.registerForm = this.fb.group({
            Email: ["", Validators.required],
            Password: ["", Validators.required],
            UserName: [""]
        });
    }

    //Event when user click sign up a new account
    OnSignup() {
        this.authenticareService.register(this.registerData).then(
            (data: any) => {
                this.notificationService.emitter.emit({ severity: 'success', summary: 'Register', detail: 'Register Successfully' });
                this.router.navigate(['/signin']);
            },
            (error: any) => {
                this.notificationService.emitter.emit({ severity: 'error', summary: 'Register', detail: 'Register Failed' });
            }
        );
    }


    //Event when user click change to login page
    ChangeToSignIn() {
        this.router.navigate(["/signin"]);
    }
}