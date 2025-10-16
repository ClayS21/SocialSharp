import { inject, Injectable, signal } from '@angular/core';
import { LoginCredentials, RegisterCredentials, User } from '../interfaces/user';
import { environment } from '../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AccountService {
    private http = inject(HttpClient);
    currentUser = signal<User | null>(null);
    private baseURL = environment.apiUrl;

    register(registerCredentials: RegisterCredentials) {
        return this.http.post<User>(`${this.baseURL}account/register`, registerCredentials, { withCredentials: true }).pipe(
            tap(user => {
                if (user) {
                    this.currentUser.set(user)
                }
            }))

    }

    login(credentials: LoginCredentials) {
        return this.http.post<User>(`${this.baseURL}account/login`, credentials, { withCredentials: true }).pipe(
            tap(user => {
                if (user) {
                    this.currentUser.set(user)
                }
            })
        )
    }
}
