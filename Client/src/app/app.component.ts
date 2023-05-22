import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component, OnInit } from '@angular/core';
import { AccountService } from './Services/account.service';
import { User } from './Interfaces/User/user';
import { NavigationStart, Router, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'Client';
  constructor(
    private http: HttpClient,
    private accountService: AccountService,
    private router: Router
  ) {
    router.events.subscribe((event) => {
      if (event instanceof NavigationStart) {
        this.accountService.currentUser$.subscribe({
          next: (user) => {
            if (user) {
              let date = new Date().getTime();
              if (user.expiration - date / 1000 < 40) {
                this.accountService
                  .refreshToken(user.refreshToken, user.accessToken)
                  .subscribe({
                    next: (s) => {},
                    error: (err) => accountService.logout(),
                  });
              }
            }
          },
        });
      }
    });
  }

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const x = localStorage.getItem('user');
    if (!x) return;
    const user: User = JSON.parse(x);
    this.accountService.setCurrentUser(user);
  }
}
