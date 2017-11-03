import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
//Register Root route
const appRoutes: Routes = [
    { path: '', redirectTo: '/signin', pathMatch: 'full' }
];

@NgModule({
    imports: [
        RouterModule.forRoot(
            appRoutes
        )
    ],
    exports: [
        RouterModule
    ],
})
export class AppRoutesModule { }