import { Component, OnInit } from "@angular/core";
import { Router, NavigationStart, NavigationEnd } from "@angular/router";

import { AccountService, Admin } from "./Services/account.service";

@Component({
  selector: "app-root",
  template: "<router-outlet></router-outlet>",
})
export class AppComponent implements OnInit {
  title = "Hospital System Admin";

  constructor(private router: Router, private accountService: AccountService) {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationStart) {
        this.accountService.currentUser$.subscribe({
          next: (user) => {
            if (user) {
              let date = new Date().getTime();
              if (!(user.expiration - date / 1000 < 40)) {
                accountService.logout();
              }
            } else {
              this.router.navigate(["/login"]);
            }
          },
        });
      }

      if (!(event instanceof NavigationEnd)) {
        return;
      }
    });
  }

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const x = localStorage.getItem("user");
    if (!x) return;
    const admin: Admin = JSON.parse(x);
    this.accountService.setCurrentUser(admin);
  }
}
