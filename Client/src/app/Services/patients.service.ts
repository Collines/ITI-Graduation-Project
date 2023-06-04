import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Headers } from '../utils/headers.utils';

@Injectable({
  providedIn: 'root',
})
export class PatientsService {
  constructor(private http: HttpClient, private headers: Headers) {}

  private BaseURL: string = 'http://35.204.41.209:7035/api/patient';
  private Headers: HttpHeaders = this.headers.getHeaders();

  GetPatientsCount() {
    return this.http.get<number>(`${this.BaseURL}/Count`, {
      headers: this.Headers,
    });
  }
}
