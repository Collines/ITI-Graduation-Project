import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AccountService } from '../Services/account.service';

@Injectable({
  providedIn: 'root',
})
export class Headers {
  private Header: HttpHeaders = new HttpHeaders()
    .set('content-type', 'application/json')
    .set('Access-Control-Allow-Origin', '*')
    .set('Lang', localStorage.getItem('language') || 'en');
  constructor(private accountServices: AccountService) {
    this.accountServices.currentUser$.subscribe({
      next: (user) => {
        if (user) {
          this.Header = this.Header.set(
            'Authorization',
            `Bearer ${user.accessToken}`
          );
        }
      },
    });
  }
  getHeaders() {
    return this.Header;
  }
}
