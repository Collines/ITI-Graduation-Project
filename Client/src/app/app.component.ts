import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from './Services/account.service';
import { User } from './Interfaces/user';
import { JsonPipe } from '@angular/common';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'Client';
  constructor(
    private http: HttpClient,
    private accountService: AccountService
  ) {}
  ngOnInit(): void {
    this.accountService.currentUser$.subscribe({
      next: (user) => {
        if (user) {
          let date = new Date().getTime();
          if (user.expiration - date / 1000 < 40) {
            this.accountService.refreshToken(user.refreshToken).subscribe({
              next: (s) => {
                console.log(s);
              },
              error: (err) => console.log(err),
            });
          }
        }
      },
    });
    this.setCurrentUser();
  }
  setCurrentUser() {
    const x = localStorage.getItem('user');
    if (!x) return;
    const user: User = JSON.parse(x);
    this.accountService.setCurrentUser(user);
  }
}
