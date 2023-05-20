import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../Interfaces/user';
import { BehaviorSubject, Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private CurrentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.CurrentUserSource.asObservable();
  BaseURL: string = 'https://localhost:7035/api/patient/';
  constructor(private http: HttpClient) {}
  login(model: any) {
    return this.http.post<User>(this.BaseURL + 'login', model).pipe(
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
    return this.http.post<User>(this.BaseURL + 'register', model).pipe(
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
  }

  // refreshToken(): any {
  //   let user = localStorage.getItem('user');
  //   let date = new Date().getTime();
  //   let parsedUser: User = user ? JSON.parse(user) : null;
  //   if (parsedUser) {
  //     if (parsedUser.expiration - date / 1000 < 30) {
  //       return this.http
  //         .post<User>(this.BaseURL + 'refresh', {
  //           RefreshToken: parsedUser.refreshToken,
  //         })
  //         .pipe(
  //           map((res: User) => {
  //             const user = res;
  //             if (user) {
  //               localStorage.setItem('user', JSON.stringify(user));
  //               this.CurrentUserSource.next(user);
  //               console.log(`token refreshed ${user}`);
  //             }
  //           })
  //         );
  //     }
  //   }
  // }
  refreshToken(refreshToken: String) {
    return this.http
      .post<User>(this.BaseURL + 'refresh', {
        RefreshToken: refreshToken,
      })
      .pipe(
        map((res: User) => {
          const user = res;
          if (user) {
            localStorage.setItem('user', JSON.stringify(user));
            this.CurrentUserSource.next(user);
            console.log(`token refreshed ${user}`);
          }
        })
      );
  }

  setCurrentUser(user: User) {
    this.CurrentUserSource.next(user);
  }
}
