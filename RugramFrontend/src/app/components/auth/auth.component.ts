import {Component} from '@angular/core';
import {animate, animation, keyframes, style} from "@angular/animations";

@Component({
    selector: 'app-auth',
    templateUrl: './auth.component.html',
    styleUrls: ['./auth.component.css'],
})

export class AuthComponent {
    showLogin = true;
    showRegister = false;
    showRegisterEnd = false;
    showLoginAnimation = true;

    toDefault() {
        this.showLogin = false;
        this.showRegister = false;
        this.showRegisterEnd = false;
    }

    onPressRegister() {
        this.toDefault()
        this.showRegister = true;
    }

    onPressReturn() {
        this.toDefault()
        this.showLogin = true;
        this.showLoginAnimation = false;
    }

    onPressNext() {
        this.toDefault()
        this.showRegisterEnd = true;
    }
}


