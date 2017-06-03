import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { NullComponent } from './../../app/null.component';
import { HelloComponent } from './../components/hello.component';
import { SignInComponent } from './../components/signin.component';
import { SignupComponent } from './../components/signup.component';
import { MainPageComponent } from './../components/main-page.component';
import { TestMQTTComponent } from './../components/testMQTT.component';

const routes: Routes = [
    { path: '', component: NullComponent },
    { path: 'hello', component: HelloComponent },
    { path: 'signin', component: SignInComponent },
    { path: 'signup', component: SignupComponent },
    { path: 'main', component: MainPageComponent },
    { path: 'mqtt', component: TestMQTTComponent }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class MainRoutesModule { }