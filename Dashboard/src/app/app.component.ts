import { Component, OnInit } from "@angular/core";
import { Router, NavigationStart, NavigationEnd } from "@angular/router";
import { IconSetService } from '@coreui/icons-angular';
import { iconSubset } from './icons/icon-subset';
import { Title } from '@angular/platform-browser';
import { AccountService, Admin } from "./Services/account.service";

@Component({
  selector: "app-root",
  template: "<router-outlet></router-outlet>",
})
export class AppComponent implements OnInit {
  title = "Hospital System Admin";

  constructor(private router: Router,
    private accountService: AccountService,
    private titleService: Title,
    private iconSetService: IconSetService) {

      titleService.setTitle(this.title);
      // iconSet singleton
      iconSetService.icons = { ...iconSubset };

    this.router.events.subscribe((event) => {
      if (event instanceof NavigationStart) {
        this.accountService.currentUser$.subscribe({
          next: (admin) => {
            if (admin) {
              let date = new Date().getTime();
              if (!(admin.expiration - date / 1000 < 40)) {
                accountService.logout();
              }
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
    const x = localStorage.getItem("admin");
    if (!x) return;
    const admin: Admin = JSON.parse(x);
    this.accountService.setCurrentUser(admin);
  }
}
