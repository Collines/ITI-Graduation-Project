import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../Interfaces/User/user';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { UserUpdate } from '../Interfaces/User/userUpdate';
import { Router } from '@angular/router';
import { UserEdit } from '../Interfaces/User/UserEdit';
import { HeaderService } from './header.service';
import { Location } from '@angular/common';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private CurrentUserSource = new BehaviorSubject<User | null>(null);
  private BaseURL: string = 'https://medical-api.creteagency.com/api/patient/';
  private Header: HttpHeaders = this.header.Header;
  currentUser$ = this.CurrentUserSource.asObservable();

  constructor(
    private http: HttpClient,
    private router: Router,
    private header: HeaderService,
    private location: Location
  ) {}

  login(model: any) {
    return this.http
      .post<User>(this.BaseURL + 'login', model, { headers: this.Header })
      .pipe(
        map((res: User) => {
          const user = res;
          if (user) {
            localStorage.setItem('user', JSON.stringify(user));
            this.CurrentUserSource.next(user);
          }
        })
      );
  }
  register(model: any) {
    return this.http
      .post<User>(this.BaseURL + 'register', model, {
        headers: this.Header,
      })
      .pipe(
        map((res: User) => {
          const user = res;
          if (user) {
            localStorage.setItem('user', JSON.stringify(user));
            this.CurrentUserSource.next(user);
          }
        })
      );
  }
  logout() {
    localStorage.removeItem('user');
    this.CurrentUserSource.next(null);
    this.location.back();
  }

  update(accessToken: string, model: FormData) {
    return this.http.patch(
      this.BaseURL + 'update?accessToken=' + accessToken,
      model,
      {
        headers: new HttpHeaders()
          .set('Access-Control-Allow-Origin', '*')
          .set('Lang', localStorage.getItem('language') || 'en')
          .set('Authorization', `bearer ${accessToken}`),
      }
    );
  }

  getData(token: string) {
    return this.http.post<UserEdit>(
      this.BaseURL + 'GetPatientData',
      `\"${token}\"`,
      {
        headers: this.Header.set('Authorization', `bearer ${token}`),
      }
    );
  }

  refreshToken(refreshToken: String, accessToken: string) {
    return this.http
      .post<User>(
        this.BaseURL + 'refresh',
        {
          RefreshToken: refreshToken,
        },
        {
          headers: this.Header.set('Authorization', `bearer ${accessToken}`),
        }
      )
      .pipe(
        map((res: User) => {
          const user = res;
          if (user) {
            localStorage.setItem('user', JSON.stringify(user));
            this.CurrentUserSource.next(user);
            console.log(`token refreshed ${user.accessToken}`);
          }
        })
      );
  }

  setCurrentUser(user: User) {
    this.CurrentUserSource.next(user);
  }
}
