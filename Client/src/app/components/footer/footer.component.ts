import { Component, OnInit } from '@angular/core';
import { NavigationEnd, NavigationStart, Router } from '@angular/router';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css'],
})
export class FooterComponent implements OnInit {
  showFooter: boolean = true;
  constructor(private router: Router) {
    if (location.pathname == '/login' || location.pathname == '/register')
      this.showFooter = false;
  }
  ngOnInit(): void {
    this.router.events.subscribe((v) => {
      if (v instanceof NavigationEnd || v instanceof NavigationStart) {
        if (v.url == '/login' || v.url == '/register') this.showFooter = false;
        else this.showFooter = true;
      }
    });
  }
}
