import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpModule } from '@angular/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@NgModule({
    imports: [FormsModule, ReactiveFormsModule, CommonModule],
    exports: [CommonModule, RouterModule],
    declarations: [],
    providers: [],
})
export class ShareModule { }
