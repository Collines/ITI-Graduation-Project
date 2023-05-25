import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, map } from "rxjs";
import { Router } from "@angular/router";

export interface Admin {
  id: number;
  userName: string;
  accessToken: string;
  refreshToken: string;
  expiration: number;
}

@Injectable({
  providedIn: "root",
})
export class AccountService {
  private CurrentUserSource = new BehaviorSubject<Admin | null>(null);
  private BaseURL: string = "https://localhost:7035/api/Admin/";
  private Header: HttpHeaders = new HttpHeaders()
    .set("content-type", "application/json")
    .set("Access-Control-Allow-Origin", "*");
  currentUser$ = this.CurrentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) {}

  login(model: any) {
    return this.http
      .post<Admin>(this.BaseURL + "login", model, { headers: this.Header })
      .pipe(
        map((result: Admin) => {
          const admin = result;
          if (admin) {
            localStorage.setItem("admin", JSON.stringify(admin));
            this.CurrentUserSource.next(admin);
            console.log(this.currentUser$);
          }
        })
      );
  }

  logout() {
    localStorage.removeItem("admin");
    this.CurrentUserSource.next(null);
    this.router.navigate(["/login"]);
  }

  setCurrentUser(admin: Admin) {
    this.CurrentUserSource.next(admin);
  }
}
