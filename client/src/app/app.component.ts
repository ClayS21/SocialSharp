import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
})
export class AppComponent implements OnInit{
    title = 'Social#';
    users: any;

    constructor(private http: HttpClient) {
    }

    ngOnInit() {
        this.getUsers();
    }

    getUsers() {
        this.http.get('https://localhost:5001/api/users').subscribe(
            {
                next: (response) => {
                    this.users = response;
                },
                error: (message) => {
                    console.log(message);
                }
            }
        )
    }
}