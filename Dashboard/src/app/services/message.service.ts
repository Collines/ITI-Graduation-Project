import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import { Headers } from "./Header";
import { Message } from "./../interfaces/Message";

@Injectable({
  providedIn: "root",
})
export class MessageService {
  private BaseURL: string = "http://35.204.41.209:7035/api/message";
  private Header: Headers = new Headers();
  constructor(private http: HttpClient, private router: Router) {}
  GetAll() {
    return this.http.get<Message[]>(this.BaseURL, {
      headers: this.Header.getHeaders(),
    });
  }

  GetById(id: number) {
    return this.http.get<Message>(this.BaseURL + `/${id}`, {
      headers: this.Header.getHeaders(),
    });
  }

  Delete(id: number) {
    return this.http.delete(this.BaseURL + `/${id}`, {
      headers: this.Header.getHeaders(),
    });
  }

  ChangeStatus(id: number, msg: Message) {
    return this.http.patch(this.BaseURL + `/${id}`, msg, {
      headers: this.Header.getHeaders(),
    });
  }
}
