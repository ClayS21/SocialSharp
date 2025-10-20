import { inject, Injectable } from '@angular/core';
import { AccountService } from './account-service';
import { tap } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
    providedIn: 'root'
})
export class InitService {
    private accountService = inject(AccountService);
    private router = inject(Router);

    start() {
        return this.accountService.refreshToken().pipe(
            tap(user => {
                if (user) {
                    this.accountService.currentUser.set(user);
                    this.router.navigateByUrl('home');
                }
            })
        )
    }

}