import { Headers } from 'src/app/services/Header';
import { IAdmin } from './../interfaces/IAdmin';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, map } from "rxjs";
import { Router } from "@angular/router";

@Injectable({
  providedIn: "root",
})
export class AccountService {
  private BaseURL: string = "http://35.204.41.209:7035/api/Admin/";
  private Header :Headers = new Headers();

  constructor(private http: HttpClient, private router: Router) {}

  login(model: any) {
    return this.http
      .post<IAdmin>(this.BaseURL + "login", model, { headers: this.Header.getHeaders() })
      .pipe(
        map((result: IAdmin) => {
          const admin = result;
          if (admin) {
            this.setLocalStorage(admin);
            this.router.navigate(["/"]);
          }
        })
      );
  }

  private setLocalStorage(admin: IAdmin) {
    localStorage.setItem("admin", JSON.stringify(admin));
  }

  private clearLocalStorage() {
    localStorage.removeItem("admin");
  }

  getAdmin() {
    const temp = localStorage.getItem("admin");
    if (!temp) return;
    const admin: IAdmin = JSON.parse(temp);
    return admin;
  }

  logout() {
    this.clearLocalStorage();
    this.router.navigate(["/login"]);
  }
}
