import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TaskComponent } from './task/task.component';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { ProfileComponent } from './profile/profile.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {HttpClientModule} from "@angular/common/http";
import {AuthInterceptorProvider} from "./interceptors/auth.interceptor";
import {ErrorInterceptorProvider} from "./interceptors/error.interceptor";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatTableModule} from "@angular/material/table";
import {MatSortModule} from "@angular/material/sort";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {MatPaginatorModule} from "@angular/material/paginator";
import { LotAllListComponent } from './lot-all-list/lot-all-list.component';
import { LotViewComponent } from './lot-view/lot-view.component';
import {MatButtonModule} from "@angular/material/button";
import { LotAddComponent } from './lot-add/lot-add.component';
import {MatSelectModule} from "@angular/material/select";
import {MatDatepickerModule} from "@angular/material/datepicker";
import {MatNativeDateModule} from "@angular/material/core";
import {CarouselModule} from "@coreui/angular";
import {NgbCarouselModule} from "@ng-bootstrap/ng-bootstrap";

@NgModule({
  declarations: [
    AppComponent,
    TaskComponent,
    LoginComponent,
    SignupComponent,
    ProfileComponent,
    LotAllListComponent,
    LotViewComponent,
    LotAddComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatTableModule,
    MatSortModule,
    MatFormFieldModule,
    MatInputModule,
    MatPaginatorModule,
    MatButtonModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    CarouselModule,
    NgbCarouselModule
  ],
  providers: [
    AuthInterceptorProvider,
    ErrorInterceptorProvider
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}


