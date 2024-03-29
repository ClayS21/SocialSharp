import { Component, Input, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable, take } from 'rxjs';
import { User } from '../_models/user';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-nav',
    templateUrl: './nav.component.html',
    styleUrl: './nav.component.css',
})
export class NavComponent implements OnInit {
    @Input() appTitle
    model: any = {}

    constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService) {}

    ngOnInit(): void {
    }

    login() {
        this.accountService.login(this.model).subscribe(
            {
                next: response => {
                    this.router.navigateByUrl('/members');
                }
            }
        )
    }

    logout() {
        this.accountService.logout();
        this.router.navigateByUrl('/');
    }
}