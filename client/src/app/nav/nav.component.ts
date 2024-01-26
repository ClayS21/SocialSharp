import { Component, Input, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable } from 'rxjs';
import { User } from '../_models/user';

@Component({
    selector: 'app-nav',
    templateUrl: './nav.component.html',
    styleUrl: './nav.component.css',
})
export class NavComponent implements OnInit {
    @Input() title
    model: any = {}

    constructor(public accountService: AccountService) {}

    ngOnInit(): void {
    }

    login() {
        this.accountService.login(this.model).subscribe(
            {
                next: response => {
                    console.log(response);
                },
                error: e => {
                    console.log(e);
                }
            }
        )
    }

    logout() {
        this.accountService.logout();
    }
}