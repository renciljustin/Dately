import { AuthGuard } from './guards/auth.guard';
import { TextTransformDirective } from './components/shared/directives/text-transform.directive';
import { AuthService } from './services/auth.service';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { NavigationComponent } from './components/navigation/navigation.component';
import { RegisterComponent } from './components/register/register.component';
import { UserDetailComponent } from './components/users/user-detail/user-detail.component';
import { UserListComponent } from './components/users/user-list/user-list.component';

import { TokenService } from './services/token.service';


import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    NavigationComponent,
    RegisterComponent,
    UserDetailComponent,
    UserListComponent,
    TextTransformDirective
  ],
  imports: [
    AppRoutingModule,
    BrowserAnimationsModule,
    BrowserModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    BsDatepickerModule.forRoot(),
    BsDropdownModule.forRoot()
  ],
  providers: [
    AuthService,
    TokenService,
    AuthGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
