import { Component, inject } from '@angular/core';
import { AccountService } from '../../services/account-service';
import { Router } from '@angular/router';

@Component({
    selector: 'home',
    imports: [],
    templateUrl: './home.html',
    styleUrl: './home.css'
})
export class Home {
    protected accountService = inject(AccountService);
    protected user = this.accountService.currentUser();
    protected router = inject(Router);

    logout() {
        this.accountService.logout().subscribe({
            next: () => {
                this.router.navigateByUrl('/login');
            }
        })
    }

}