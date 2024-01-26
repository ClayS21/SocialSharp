import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrl: './home.component.css',
})
export class HomeComponent implements OnInit{

    constructor() {}

    ngOnInit(): void {}

    registerMode = false;

    registerToggle() {
        this.registerMode = !this.registerMode;
    }

    cancelRegisterMode(event: boolean)
    {
        this.registerMode = event;
    }
}
