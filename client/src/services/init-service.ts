import { inject, Injectable } from '@angular/core';
import { AccountService } from './account-service';

@Injectable({
    providedIn: 'root'
})
export class InitService {
    private accountService = inject(AccountService);

    start() {
        this.accountService.refreshToken().subscribe({
            next: user => {
                this.accountService.currentUser.set(user);
            }
        })
    }

}
