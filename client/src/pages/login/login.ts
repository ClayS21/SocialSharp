import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'login',
    imports: [FormsModule],
    templateUrl: './login.html',
    styleUrl: './login.css'
})
export class Login {
    private http = inject(HttpClient);
    protected credentials: any = {}

    login()
    {
        this.http.post('https://localhost:7060/api/account/login', this.credentials).subscribe({
            next: response => console.log(response)
        })
    }
}
