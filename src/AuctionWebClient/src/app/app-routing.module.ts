import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {TaskComponent} from "./task/task.component";
import {LoginComponent} from "./login/login.component";
import {AuthGuard} from "./helpers/auth.guard";
import {SignupComponent} from "./signup/signup.component";
import {ProfileComponent} from "./profile/profile.component";
import {AppComponent} from "./app.component";
import {LotAllListComponent} from "./lot-all-list/lot-all-list.component";
import {LotViewComponent} from "./lot-view/lot-view.component";
import {LotAddComponent} from "./lot-add/lot-add.component";

const routes: Routes = [
  { path: 'home', component: AppComponent },
  { path: 'task', component: TaskComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: SignupComponent },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'lots/all', component: LotAllListComponent },
  { path: 'lot/view', component: LotViewComponent },
  { path: 'lot/add', component: LotAddComponent },
  { path: '', redirectTo: 'task', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
