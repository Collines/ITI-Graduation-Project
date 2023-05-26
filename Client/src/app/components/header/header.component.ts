import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent {
  showSlider: boolean = true;
  constructor(private router: Router) {
    this.router.events.subscribe((v) => {
      if (v instanceof NavigationEnd) {
        if (v.url == '/login' || v.url == '/register' || v.url == '/dashboard')
          this.showSlider = false;
      }
    });
  }
}
