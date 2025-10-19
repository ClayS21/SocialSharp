import { inject, Injectable } from '@angular/core';
import { AccountService } from './account-service';
import { tap } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class InitService {
    private accountService = inject(AccountService);

    start() {
        return this.accountService.refreshToken().pipe(
            tap(user => {
                if (user) {
                    this.accountService.currentUser.set(user);
                }
            })
        )
    }

}