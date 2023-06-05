import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Headers } from '../utils/headers.utils';
import { Message } from '../Interfaces/Message';

@Injectable({
  providedIn: 'root',
})
export class MessageService {
  constructor(
    private http: HttpClient,
    private router: Router,
    private header: Headers
  ) {}
  private BaseURL: string = 'https://medical-api.creteagency.com/api/message';
  private Header: HttpHeaders = this.header.getHeaders();

  Add(msg: Message) {
    return this.http.post(this.BaseURL, msg, { headers: this.Header });
  }
}
