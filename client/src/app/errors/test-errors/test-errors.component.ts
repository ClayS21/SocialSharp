import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-test-errors',
    templateUrl: './test-errors.component.html',
    styleUrl: './test-errors.component.css',
})
export class TestErrorsComponent implements OnInit{
    baseUrl = 'https://localhost:5001/api/';
    validationErrors: string[] = []

    constructor(private http: HttpClient) {}

    ngOnInit(): void {
    }

    get404error() {
        this.http.get(this.baseUrl + 'buggy/not-found').subscribe(
            {
                next: response => 
                {
                    console.log(response);
                },
                error: err => 
                {
                    console.log(err)
                }
            }
        )
    }

    get400error() {
        this.http.get(this.baseUrl + 'buggy/bad-request').subscribe(
            {
                next: response => console.log(response),
                error: err => console.log(err)
            }
        )
    }

    get500error() {
        this.http.get(this.baseUrl + 'buggy/server-error').subscribe(
            {
                next: response => console.log(response),
                error: err => console.log(err)
            }
        )
    }

    get401error() {
        this.http.get(this.baseUrl + 'buggy/auth').subscribe(
            {
                next: response => console.log(response),
                error: err => console.log(err)
            }
        )
    }

    get400ValidationError() {
        this.http.post(this.baseUrl + 'account/register', {}).subscribe(
            {
                next: response => console.log(response),
                error: err => 
                {
                    console.log(err);
                    this.validationErrors = err;
                }
            }
        )
    }
}
