import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LoginComponent} from "./login/login.component";
import {AuthGuard} from "./helpers/auth.guard";
import {SignupComponent} from "./signup/signup.component";
import {ProfileComponent} from "./profile/profile.component";
import {LotAllListComponent} from "./lot-all-list/lot-all-list.component";
import {LotAddComponent} from "./lot-add/lot-add.component";
import {LotByUserCreateComponent} from "./lot-by-user-create/lot-by-user-create.component";
import {LotByUserBuyComponent} from "./lot-by-user-buy/lot-by-user-buy.component";

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: SignupComponent },
  { path: 'lot/all', component: LotAllListComponent },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'lot/add', component: LotAddComponent, canActivate: [AuthGuard] },
  { path: 'lot/published', component: LotByUserCreateComponent, canActivate: [AuthGuard] },
  { path: 'lot/bought', component: LotByUserBuyComponent, canActivate: [AuthGuard] },
  { path: '', redirectTo: 'lot/all', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
