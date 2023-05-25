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
  private CurrentUser:(Admin|null) = null;
  private BaseURL: string = "https://localhost:7035/api/Admin/";
  private Header: HttpHeaders = new HttpHeaders()
    .set("content-type", "application/json")
    .set("Access-Control-Allow-Origin", "*");

  constructor(private http: HttpClient, private router: Router) {}

  login(model: any) {
    return this.http
      .post<Admin>(this.BaseURL + "login", model, { headers: this.Header })
      .pipe(
        map((result: Admin) => {
          const admin = result;
          if (admin) {
            this.setLocalStorage(admin);
            this.router.navigate(["/"]);
          }
        })
      );
  }

  private setLocalStorage(admin: Admin) {
    localStorage.setItem("admin", JSON.stringify(admin));
    this.CurrentUser = admin;
  }

  private clearLocalStorage() {
    localStorage.removeItem("admin");
  }

  getAdmin() {
    const temp = localStorage.getItem("admin");
    if (!temp) return;
    const admin: Admin = JSON.parse(temp);
    if(admin.accessToken != this.CurrentUser?.accessToken) 
      return; 
    return admin;
  }

  logout() {
    this.clearLocalStorage();
    this.CurrentUser = null;
    this.router.navigate(["/login"]);
  }
}
