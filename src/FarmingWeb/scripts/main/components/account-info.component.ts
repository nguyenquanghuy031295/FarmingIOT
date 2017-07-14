import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { Router } from '@angular/router'
import { FormGroup, Validators, FormBuilder } from "@angular/forms";

import { AccountInfoModel } from './../models/account-info.model';

import { IAuthenticateService } from './../services/interface/authenticate.-service.interface';
import { NotificationService } from './../services/impl/notification.service';
@Component({
    selector: 'account-info',
    templateUrl: './templates/main/components/account-info.component.html'
})
export class AccountInfoComponent implements OnInit {
    public accountForm: FormGroup;
    public userInfo: AccountInfoModel = new AccountInfoModel();
    private locateDOB: string = "";
    private isDisableSubmit: boolean = false;
    constructor(
        private router: Router,
        private fb: FormBuilder,
        private http: Http,
        private authenticateService: IAuthenticateService,
        private notificationService: NotificationService
    ) { }

    ngOnInit() {
        this.accountForm = this.fb.group({
            Email: [{ value: '', disabled: true }],
            DOB: [""],
            Name: [""],
            Address: [""]
        });
        this.authenticateService.getAccontInfo().then(
            (userInfo: AccountInfoModel) => {
                this.userInfo = userInfo;
                if (userInfo.DOB)
                    this.locateDOB = userInfo.DOB.toLocaleString().slice(0, 10);
            },
            (error: any) => {

            }
        );
    }

    onChangeInfo() {
        this.isDisableSubmit = true;
        this.authenticateService.editAccountInfo(this.userInfo).then(
            (data: any) => {
                this.notificationService.emitter.emit({ severity: 'success', summary: 'Update Info', detail: 'Update Successfully' });
                this.router.navigate(['farmiot/main']);
            },
            (error: any) => {
                this.isDisableSubmit = false;
            }
        );
    }

    onChangeDOB(event: any) {
        this.userInfo.DOB = new Date(event);
    }
}