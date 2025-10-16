import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AccountService } from '../../services/account-service';
import { RegisterCredentials } from '../../interfaces/user';

@Component({
    selector: 'register',
    imports: [ReactiveFormsModule],
    templateUrl: './register.html',
    styleUrl: './register.css'
})
export class Register {
    protected accountService = inject(AccountService);
    protected registerForm = new FormGroup({
        firstName: new FormControl('', Validators.required),
        lastName: new FormControl('', Validators.required),
        email: new FormControl('', [Validators.required, Validators.email]),
        password: new FormControl('', [Validators.required, Validators.minLength(8)]),
        dateOfBirth: new FormControl('', Validators.required),
        gender: new FormControl('', Validators.required)
    })

    register() {
        if (this.registerForm.valid) {
            this.accountService.register(this.registerForm.value as RegisterCredentials).subscribe({
                next: user => console.log(user)
            })
        }
    }

}