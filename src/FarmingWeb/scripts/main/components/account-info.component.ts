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
    public resetPasswordForm: FormGroup;
    public userInfo: AccountInfoModel = new AccountInfoModel();
    private locateDOB: string = "";
    private isDisableSubmit: boolean = false;

    public newPassword: string = "";
    public confirmPassword: string = "";
    public currentPassword: string = "";

    public isShowMatched: boolean = null;
    public _matched: boolean = true;
    constructor(
        private router: Router,
        private fb: FormBuilder,
        private http: Http,
        private authenticateService: IAuthenticateService,
        private notificationService: NotificationService
    ) {
        this.resetPasswordForm = this.fb.group({
            CurrentPassword: ["", Validators.required],
            NewPassword: ["", [Validators.required, Validators.minLength(6)]],
            ConfirmPassword: ["", Validators.required]
        });
    }

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

    checkMatched() {
        let confimElement: Element = document.getElementById("confirmPass");
        this._matched = (this.newPassword === this.confirmPassword) && (this.newPassword.length != 0);
        if (this.newPassword == this.confirmPassword && this.newPassword.length == 0) {
            confimElement.className = "form-group has-feedback";
            this.isShowMatched = null;
        }
        else if (this._matched && this.resetPasswordForm.controls["ConfirmPassword"].dirty && this.resetPasswordForm.controls["NewPassword"].valid) {
            this.isShowMatched = true;
            confimElement.className = "form-group has-success has-feedback";
        }
        else if (!this._matched && this.resetPasswordForm.controls["ConfirmPassword"].dirty) {
            this.isShowMatched = false;
            confimElement.className = "form-group has-error has-feedback";
        }
    }

    OnChangePassword() {
        this.authenticateService.changePassword(this.currentPassword, this.newPassword).then(
            (data: any) => {
                this.notificationService.emitter.emit({ severity: 'success', summary: 'Change Password', detail: 'Change Password Successfully' });
                this.router.navigate(['farmiot/main']);
            },
            (error: any) => {
                this.notificationService.emitter.emit({ severity: 'success', summary: 'Change Password', detail: 'Change Password Failed' });
            }
        );
    }
}