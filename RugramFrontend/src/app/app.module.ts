import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {AppComponent} from './app.component';
import {FormsModule} from "@angular/forms";
import {AuthComponent} from './components/auth/auth.component';
import {LoginComponent} from './components/auth/components/login/login.component';
import {RegisterComponent} from './components/auth/components/register/register.component';
import {RegisterEndComponent} from './components/auth/components/register-end/register-end.component';
import {HttpClientModule} from "@angular/common/http";

@NgModule({
    declarations: [
        AppComponent,
        AuthComponent,
        LoginComponent,
        RegisterComponent,
        RegisterEndComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        HttpClientModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule {
}
