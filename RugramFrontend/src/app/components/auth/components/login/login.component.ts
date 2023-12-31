import {Component, EventEmitter ,OnInit, Output} from '@angular/core';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {



    @Output()
    onPressRegister = new EventEmitter();

    showRegister() {
        this.onPressRegister.emit();
    }

    ngOnInit(): void {

    }
}
