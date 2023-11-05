import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
    selector: 'app-register',
    templateUrl: 'register.component.html',
    styleUrls: ['register.component.css']
})

export class RegisterComponent implements OnInit {

    constructor(public http: HttpClient) {
    }

    @Output()
    onPressNext = new EventEmitter();
    @Output()
    onPressReturn = new EventEmitter();

    showNext() {
        const requestBody = {email: 'salavatyllinilfat@mail.ru'};
        this.http.post('http://localhost:5062/auth/confirm-email', requestBody).subscribe();
        this.onPressNext.emit();
    }

    showLogin() {
        this.onPressReturn.emit();
    }

    ngOnInit(): void {

    }

}

