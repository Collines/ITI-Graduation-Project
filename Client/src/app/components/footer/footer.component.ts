import { Component } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css'],
})
export class FooterComponent {
  showFooter: boolean = true;
  constructor(private router: Router) {
    this.router.events.subscribe((v) => {
      if (v instanceof NavigationEnd) {
        if (v.url == '/login' || v.url == '/register' || v.url == '/dashboard')
          this.showFooter = false;
        else this.showFooter = true;
      }
    });
  }
}
