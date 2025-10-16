import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { LoginCredentials } from '../../interfaces/user';
import { AccountService } from '../../services/account-service';

@Component({
    selector: 'login',
    imports: [FormsModule, RouterLink],
    templateUrl: './login.html',
    styleUrl: './login.css'
})
export class Login {
    protected accountService = inject(AccountService)
    protected router = inject(Router)
    protected credentials = {} as LoginCredentials

    login() {
        this.accountService.login(this.credentials).subscribe({
            next: () => {
                this.router.navigateByUrl('home');
            }
        })

    }
}
