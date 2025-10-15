import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
    selector: 'register',
    imports: [ReactiveFormsModule],
    templateUrl: './register.html',
    styleUrl: './register.css'
})
export class Register {
    private http = inject(HttpClient);
    protected registerForm = new FormGroup({
        firstName: new FormControl('', Validators.required),
        lastName: new FormControl('', Validators.required),
        email: new FormControl('', [Validators.required, Validators.email]),
        password: new FormControl('', [Validators.required, Validators.minLength(8)]),
        dateOfBirth: new FormControl('', Validators.required),
        gender: new FormControl('', Validators.required)
    })

    register() {
        this.http.post('https://localhost:7060/api/account/register', { ...this.registerForm.value }).subscribe({
            next: response => console.log(response)
        })
        
    }

}