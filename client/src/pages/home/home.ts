import { Component, inject, signal } from '@angular/core';
import { AccountService } from '../../services/account-service';
import { Router } from '@angular/router';
import { Navbar } from "../../components/navbar/navbar";
import { Sidebar } from "../../components/sidebar/sidebar";
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

interface Post {
        content: string;
        image: string;
    };

@Component({
    selector: 'home',
    imports: [Navbar, Sidebar, FormsModule],
    templateUrl: './home.html',
    styleUrl: './home.css'
})
export class Home {
    protected accountService = inject(AccountService);
    protected user = this.accountService.currentUser();
    protected router = inject(Router);
    protected http = inject(HttpClient);
    protected posts = signal<Post[]>([]);
    selectedFile: File | null = null;

    post = {} as Post;

    logout() {
        this.accountService.logout().subscribe({
            next: () => {
                this.router.navigateByUrl('/login');
            }
        })
    }

    sendPost() {
        if (!this.selectedFile) return;
        const formData = new FormData();
        formData.append('image', this.selectedFile);
        formData.append('content', this.post.content);
        this.http.post<Post>('https://localhost:7060/api/posts/add-post', formData).subscribe({
            next: response => {
                this.posts.update(arr => [...arr, response])
            }
        })
    }

    fileSelected(event: Event) {
        const input = event.target as HTMLInputElement;
        if (!input.files?.[0]) return;

        this.selectedFile = input.files[0];
    }

}