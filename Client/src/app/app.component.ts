import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from './Services/account.service';
import { User } from './Interfaces/User/user';
import {
  ActivatedRoute,
  NavigationEnd,
  NavigationStart,
  Router,
  RouterOutlet,
} from '@angular/router';
import { slideInAnimation } from './animations/app.animation';
import { Title } from '@angular/platform-browser';
import { filter } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  animations: [slideInAnimation],
})
export class AppComponent implements OnInit {
  loader=true;

  title = 'Client';
  constructor(
    private http: HttpClient,
    private accountService: AccountService,
    private router: Router,
    private titleService: Title,
    private activatedRoute: ActivatedRoute
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
    this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe(() => {
        const rt = this.getChild(this.activatedRoute);
        rt.data.subscribe((data) => {
          this.titleService.setTitle(data['title']);
        });
      });
  }

  getChild(activatedRoute: ActivatedRoute): ActivatedRoute {
    if (activatedRoute.firstChild) {
      return this.getChild(activatedRoute.firstChild);
    } else {
      return activatedRoute;
    }
  }

  setCurrentUser() {
    const x = localStorage.getItem('user');
    if (!x) return;
    const user: User = JSON.parse(x);
    this.accountService.setCurrentUser(user);
  }

  // prepareRoute(outlet:RouterOutlet){
  // return outlet && outlet.activatedRouteData
  // }

  loaderStop:any=setTimeout(() => {
    this.loader=false;

  }, 3000);



}
